using System.Reflection;
using System.Text;
using Wasmtime;
using Module = Wasmtime.Module;

using var engine = new Engine();

using var moduleData = Assembly.GetExecutingAssembly().GetManifestResourceStream("rust.wasm")
                       ?? throw new InvalidOperationException("The requested resource was not found.");
using var module = Module.FromStream(engine, "rust", moduleData);

using var linker = new Linker(engine);
using var store = new Store(engine);

/*
 * https://webassembly.github.io/wabt/demo/wasm2wat/ is useful for viewing the imports
 * and exports of the generated module. Note that performing any operations that rely
 * on OS functionality (such as reading files or printing to the console) will add
 * additional required imports that must have implementations defined here.
 */
linker.Define("env", "alert", Function.FromCallback(store,
    (Caller caller, int ptr, int len) =>
    {
        if (!caller.TryGetMemorySpan<byte>("memory", ptr, len, out var buf))
        {
            throw new InvalidOperationException("Failed to read string from WASM memory.");
        }

        var text = Encoding.UTF8.GetString(buf);
        Console.WriteLine(text);
    }));

var instance = linker.Instantiate(store, module);
var greet = instance.GetAction("greet") ??
            throw new InvalidOperationException("The requested function export was not found.");

greet();