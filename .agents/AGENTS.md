
## Naming and Build Conventions
- Never use hardcodes, whitelists, or regex replacements to manipulate output code during the build process (e.g., Fable TS generation). All transformations and conventions must be handled natively by compiler features (such as [<CompiledName>] or standard naming conventions).
