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
- Compile and distribute a binary for convenient prototyping (homebrew? synaptic?)

## Status

This project is not yet complete and has not yet been used for a production
website. As such, it should not be considered commercial strength

### Ongoing tasks

- [x] Set up a docker image for development (with docker compose)
- [x] Set up a docker image for running in production 
- [ ] Refactor integration tests to mount DI container in abase class
- [ ] Refactor integration tests to live in separate classes
- [ ] Remove references to attribute-based DI
- [ ] Implement more robust file path handling
- [ ] Fully implement conversion strategies for the remaining markdown tag types
- [ ] Fully implement a default template for the remaining markdown tag types
- [ ] Output YAML comments to HTML files (as comments, for debugging) (yaml attributes are already being parsed)
- [ ] Clean up DI implementation and remove references to attribute-based DI
- [ ] Implement mechanism for copying images
- [ ] Create some mechanism for implementing page "types" (pages, posts, categories, etc)
- [ ] Create a mechanism for generating a home or index page
- [ ] Set up code coverage to generate on build

## Containerization

These instructions are out of date, though they may still work.

This is an early prototype of containerization for me. You can run the project
and the tests for the project by building the docker image (from the dockerfile) 
```
    sudo docker build -t s3g-image -f Dockerfile .
```

Then you can run the build and tests by running 
```
    sudo docker run -it --rm --name s3g-container s3g-image
```

Unfortunately for the moment, you will need to re-run the build and the run
every time you make a code change.
