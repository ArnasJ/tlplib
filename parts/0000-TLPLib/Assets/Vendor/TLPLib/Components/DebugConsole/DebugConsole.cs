﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using com.tinylabproductions.TLPGame.Components.Controller;
using com.tinylabproductions.TLPLib.Concurrent;
using com.tinylabproductions.TLPLib.Data;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;
using com.tinylabproductions.TLPLib.Reactive;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.tinylabproductions.TLPLib.Components.DebugConsole {
  public class DConsole {
    public struct Command {
      public readonly string cmdGroup, name;
      public readonly Action run;

      public Command(string cmdGroup, string name, Action run) {
        this.cmdGroup = cmdGroup;
        this.name = name;
        this.run = run;
      }
    }

    struct Instance {
      public readonly DebugConsoleBinding view;

      public Instance(DebugConsoleBinding view) {
        this.view = view;
      }
    }

    public static DConsole instance { get; } = new DConsole();
    public static readonly ImmutableArray<bool> bools = ImmutableArray.Create(true, false);

    DConsole() {
      var r = registrarFor(nameof(DConsole));
      r.register("Self-test", () => "self-test");
      r.register("Future Self-test", () => Future.delay(Duration.fromSeconds(1), () => "after 1 s"));
    }

    public delegate void OnShow(DConsole console);

    readonly Dictionary<string, List<Command>> commands = new Dictionary<string, List<Command>>();
    public event OnShow onShow;

    Option<Instance> current = F.none<Instance>();

    public static readonly ImmutableList<int> DEFAULT_MOUSE_SEQUENCE = ImmutableList.Create(0, 1, 3, 2, 0, 2, 3, 1, 0);
    public static readonly ImmutableList<InputController.DPad> DEFAULT_DPAD_SEQUENCE =
      ImmutableList.Create(
        InputController.DPad.Left, InputController.DPad.Left, 
        InputController.DPad.Right, InputController.DPad.Right,
        InputController.DPad.Left, InputController.DPad.Up, 
        InputController.DPad.Right, InputController.DPad.Down,
        InputController.DPad.Right, InputController.DPad.Right
      );

    public static readonly DebugSequenceMouseData DEFAULT_MOUSE_DATA = new DebugSequenceMouseData();
    public class DebugSequenceMouseData {
      public readonly int width, height;
      public readonly ImmutableList<int> sequence;

      public DebugSequenceMouseData(int width=2, int height=2, ImmutableList<int> sequence=null) {
        this.width = width;
        this.height = height;
        this.sequence = sequence ?? DEFAULT_MOUSE_SEQUENCE;
      }
    }

    public static IObservable<Unit> registerDebugSequence(
      DebugSequenceMouseData mouseData=null, ImmutableList<InputController.DPad> controllerSequence=null,
      DebugConsoleBinding binding=null
    ) {
      binding = binding ?? Resources.Load<DebugConsoleBinding>("Debug Console Prefab");
      mouseData = mouseData ?? DEFAULT_MOUSE_DATA;
      controllerSequence = controllerSequence ?? DEFAULT_DPAD_SEQUENCE;

      var mouseObs = 
        new RegionClickObservable(mouseData.width, mouseData.height)
        .sequenceWithinTimeframe(mouseData.sequence, 3);
      var controllerObs = 
        InputController.track(InputController.dpadList, holding: false)
        .withinTimeframe(controllerSequence.Count, 5.seconds())
        .filter(l => l.Select(t => t._1).SequenceEqual(controllerSequence));

      var obs = mouseObs.joinDiscard(controllerObs);
      obs.subscribe(_ => instance.show(binding));
      return obs;
    }

    public void register(Command command) {
      var list = commands.get(command.cmdGroup).getOrElse(() => {
        var lst = new List<Command>();
        commands[command.cmdGroup] = lst;
        return lst;
      });
      list.Add(command);
    }

    public DConsoleRegistrar registrarFor(string prefix) => 
      new DConsoleRegistrar(this, prefix);

    public void registerHashSet<A>(
      string name, Ref<ImmutableHashSet<A>> pv, IEnumerable<A> options
    ) {
      var r = registrarFor(name);
      r.register("List", () => pv.value.asString());
      r.register("Clear", () => {
        pv.value = ImmutableHashSet<A>.Empty;
        return pv.value.asString();
      });
      foreach (var f in options) {
        r.register($"{f}?", () => pv.value.Contains(f));
        r.register($"Toggle {f}", () => {
          pv.value = pv.value.toggle(f);
          return $"in set={pv.value.Contains(f)}";
        });
      }
    }

    public void show(DebugConsoleBinding binding) {
      destroy();
      onShow?.Invoke(this);
      onShow = null;

      var view = binding.clone();
      Object.DontDestroyOnLoad(view);
      foreach (var commandGroup in commands) {
        var button = addButton(view.buttonPrefab, view.commandGroupsHolder.transform);
        button.text.text = commandGroup.Key;
        button.button.onClick.AddListener(() => showGroup(view, commandGroup.Key, commandGroup.Value));
      }

      Application.logMessageReceivedThreaded += onLogMessageReceived;
      view.closeButton.onClick.AddListener(destroy);

      current = new Instance(view).some();
    }

    static void showGroup(DebugConsoleBinding view, string groupName, IEnumerable<Command> commands) {
      view.commandGroupLabel.text = groupName;
      foreach (var t in view.commandsHolder.transform.children()) Object.Destroy(t.gameObject);
      foreach (var command in commands) {
        var button = addButton(view.buttonPrefab, view.commandsHolder.transform);
        button.text.text = command.name;
        button.button.onClick.AddListener(() => command.run());
      }
    }

    static ButtonBinding addButton(ButtonBinding prefab, Transform target) {
      var button = prefab.clone();
      // Parent of RectTransform is being set with parent property.
      // Consider using the SetParent method instead, with the worldPositionStays
      // argument set to false. This will retain local orientation and scale rather
      // than world orientation and scale, which can prevent common UI scaling issues.
      button.GetComponent<RectTransform>().SetParent(target, worldPositionStays: false);
      return button;
    }

    void onLogMessageReceived(string message, string stackTrace, LogType type) {
      foreach (var instance in current) {
        ASync.OnMainThread(() => {
          var entry = instance.view.logEntryPrefab.clone();
          var shortText = $"{DateTime.Now}  {type}  {message}";

          entry.text = shortText;
          entry.GetComponent<RectTransform>().SetParent(
            instance.view.logEntriesHolder.transform, worldPositionStays: false
          );
          entry.transform.SetAsFirstSibling();
        });
      }
    }

    public void destroy() {
      foreach (var instance in current) {
        Application.logMessageReceivedThreaded -= onLogMessageReceived;
        Object.Destroy(instance.view.gameObject);
      }
      current = current.none;
    }
  }

  public delegate Option<Obj> HasObjFn<Obj>();

  public struct DConsoleRegistrar {
    public readonly DConsole console;
    public readonly string commandGroup;

    public DConsoleRegistrar(DConsole console, string commandGroup) {
      this.console = console;
      this.commandGroup = commandGroup;
    }

    static readonly HasObjFn<Unit> unitSomeFn = () => F.some(F.unit);

    public void register(string name, Action run) {
      register(name, () => { run(); return F.unit; });
    }
    public void register<A>(string name, Fn<A> run) {
      register(name, unitSomeFn, _ => run());
    }
    public void register<A>(string name, Fn<Future<A>> run) {
      register(name, unitSomeFn, _ => run());
    }
    public void register<Obj>(string name, HasObjFn<Obj> objOpt, Act<Obj> run) {
      register(name, objOpt, obj => { run(obj); return F.unit; });
    }
    public void register<Obj, A>(string name, HasObjFn<Obj> objOpt, Fn<Obj, A> run) {
      register(name, objOpt, obj => Future.successful(run(obj)));
    }
    public void register<Obj, A>(string name, HasObjFn<Obj> objOpt, Fn<Obj, Future<A>> run) {
      var prefixedName = $"[DC|{commandGroup}]> {name}";
      console.register(new DConsole.Command(commandGroup, name, () => {
        var opt = objOpt();
        if (opt.isDefined) {
          var returnFuture = run(opt.get);
          Act<A> onComplete = t => Debug.Log($"{prefixedName} done: {t}");
          // Check perhaps it is completed immediately.
          returnFuture.value.voidFold(
            () => {
              Debug.Log($"{prefixedName} starting.");
              returnFuture.onComplete(onComplete);
            },
            onComplete
          );
        }
        else Debug.Log($"{prefixedName} not running: {typeof(Obj)} is None.");
      }));
    }

    public void registerToggle(string name, Ref<bool> r, string comment=null) =>
      registerToggle(name, () => r.value, v => r.value = v, comment);

    public void registerToggle(string name, Fn<bool> getter, Act<bool> setter, string comment=null) {
      register($"{name}?", getter);
      register($"Toggle {name}", () => {
        setter(!getter());
        return comment == null ? getter().ToString() : $"{comment}: value={getter()}";
      });
    }

    public void registerCountdown(string name, uint count, Action act) {
      var countdown = count;
      register(name, () => {
        countdown--;
        if (countdown == 0) {
          act();
          countdown = count;
          return $"{name} EXECUTED.";
        }
        return $"Press me {countdown} more times to execute.";
      });
    }
  }
}
