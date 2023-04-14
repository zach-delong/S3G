# Simple Static Site Generator (S3G)

This project is a simple toy project that is intended to convert a given
directory full of markdown files into a blog. Heavily inspired by projects like
[Jekyll](https://jekyllrb.com "Jekyll homepage") or [Hugo](https://gohugo.io "Hugo
homepage"). The primary difference is that I wanted something written in .NET,
and I wanted an opportunity to play with the new .NET versions that are coming
out. As of this writing, the project is written for .NET 7.

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

### Front matter

I have now added support for FrontMatter! Currently this only supports two properties, but more should be added in the future!

#### Published

If you add "published" to your front matter on a document, you can control whether or not that document will appear in your export! 

```md
---
published: false
---
this document won't be published.
```

If you do not provide a published attribute, it is assumed to be set to true.

#### Title

If you add a "title" attribute, wherever you put "{{title}}" in your site_template.html will be replaced with the content! You should probably do this in your template: `<title>{{title}}</title>`.

```md
---
title: an interesting readme
---
This page will have the title "an interesting readme"
```

### Ongoing tasks

- [X] Copy non *.md files to the output directory
- [X] Update to .NET 6
- [x] Update to .NET 7
- [X] Covert common *.md tags to HTML
- [x] Reorganize namespaces 
- [x] Implement custom titles on each "page"
- [x] Implement FluentAssertions for integration tests
- [ ] Automate docker image publishing
- [ ] Design & implement some kind of metadata (titles, publish date, etc)
- [ ] Build a homepage with treeview navigation
- [ ] Add some kind of metadata system (author, publish date, etc)
- [ ] Multithread processing so conversion can happen more quickly
