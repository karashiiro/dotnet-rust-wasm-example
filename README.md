# dotnet-rust-wasm-example
Using Rust from .NET via WASM for simplified cross-platform native (not really, since Rust can't make syscalls this way) builds.

## Building
Install [Rust](https://www.rust-lang.org/tools/install) and [WasmEdge](https://wasmedge.org/book/en/quick_start/install.html) before starting.

In the `rust` folder:
```shell
cargo build --target wasm32-wasi
```

In the `dotnet` folder:
```shell
dotnet run
```