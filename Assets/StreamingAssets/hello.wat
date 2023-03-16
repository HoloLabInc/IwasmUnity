(module
  (func $hello (import "env" "hello"))
  (func (export "run") (call $hello))
)
