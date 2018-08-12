# MarkDom

MarkDom is a Markdown parser implemented in C# using recursion and regular expressions.

An advantage to markdom is that instead of simply replacing the input Markdown with HTML output, MarkDom generates a simple Document Object Model to enable interaction with the structured Markdown data.

Another advantage to this approach is to allow transformation to formats other than HTML, such as PDF, Word, or any other formatted document.

This has not been put through the wringer yet, so if you use this, please let report any issues you find. Pull requests are welcome.

For a live demo, I'm currently dogfooding [v0.1.0](https://github.com/johnnyRose/MarkDom/releases/tag/v0.1.0) on [my personal site](https://johnrosewicz.com/)'s blog.
