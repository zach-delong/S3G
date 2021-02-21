;;;; Project-specific emacs variables

((csharp-mode . ((projectile-project-compilation-cmd . "dotnet run --project StaticSiteGenerator")
         (projectile-project-test-cmd . "dotnet test --collect 'XPlat Code Coverage'"))))
