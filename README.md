# Simple Static Site Generator (S3G)

This project is a simple toy project that is intended to convert a given
directory full of markdown files into a blog. Heavily inspired by projects like
[Jekyl](https://jekyllrb.com "Jekyl homepage") or [Hugo](https://gohugo.io "Hugo
homepage"). The primary difference is that I wanted something written in .NET,
and I wanted an opportunity to play with the new .NET versions that are coming
out. As of this writing, the project is written for .NET 5.

## Contributing

Right now, I am not taking contributions for this project.  That may change at
some point in the future, but as of right now, I do not have time to fully
maintain this project as a collaborative work. 

That said, you are welcome to fork the project and use it but it is currently 
in a prototype state, and while it has come a long way it still has quite far 
to go and I make no promises about interface stability or backwards incompatible 
changes as of now.

## Overall design

Fundamentally, this project is intended to be used with Docker (eventually) or
some other container technology as part of a build pipeline for a website. Send
in a commit to your favorite source control, then automatically trigger S3G to
compile your website into something beautiful.

## Goals

- Convert all common tags from Markdown to HTML
- Support templating (user-defined site templates)
- Compile and distribute a binary for convenient prototyping (homebrew?
synaptic?)

## Status

This project is not yet complete and has not yet been used for a production
website. As such, it should not be considered commercial strength

### Ongoing tasks

- [X] Copy non *.md files to the output directory
- [ ] Reorganize namespaces 
- [ ] Consider consolidating test projects into one project
- [ ] Covert common *.md tags to HTML
- [ ] Build a homepage with treeview navigation
