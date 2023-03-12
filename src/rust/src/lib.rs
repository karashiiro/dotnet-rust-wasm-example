// All of the functions that the host runtime provides need to be declared here.
extern "C" {
    // Strings need to be passed in FFI-compatible forms.
    // In this case, alert takes a string, which is decomposed into
    // a pointer to the start of the string and its length.
    fn alert(ptr: *const u8, len: usize);
}

// If this returned a value, that value would need to be FFI-compatible to be usable.
#[no_mangle]
pub fn greet() {
    let text = "Hello, rust!";
    unsafe { alert(text.as_ptr(), text.len()) };
}
