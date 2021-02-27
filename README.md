Welcome to FakeBASH, a simple emulator for the BASH CLI created with C#!

NOT supported:<br />
-Complex commands (commands only work using basic functionality)<br />
-Tab-completion<br />
-Scripting<br />
-Much, much more<br />

Supported commands:<br />
-cd (".", "..", "-", "~" options all work)<br />
-ls (Including -a option and support for listing multiple directories at once)<br />
-pwd<br />
-echo (Simply echos input. Does not run scripts. Includes '>' and '>>' options for editing files, however this does not work quite like the real thing. See Echo.cs for details)<br />
-touch<br />
-mkdir<br />
-grep<br />
-mv (Includes moving and renaming files)<br />
-rmdir<br />
-rm<br />
