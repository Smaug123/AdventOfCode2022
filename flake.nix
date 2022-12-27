{
  description = "Advent of Code 2022";

  inputs = {
    flake-utils.url = "github:numtide/flake-utils";
    nixpkgs.url = "nixpkgs/nixpkgs-unstable";
  };

  outputs = {
    self,
    nixpkgs,
    flake-utils,
    ...
  }:
    flake-utils.lib.eachDefaultSystem (system: let
      pkgs = nixpkgs.legacyPackages.${system};
      projectFile = "./AdventOfCode2022.App/AdventOfCode2022.App.fsproj";
      testProjectFile = "./AdventOfCode2022.Test/AdventOfCode2022.Test.fsproj";
      pname = "AdventOfCode2022";
      outputFiles = [""];
      arrayToShell = a: toString (map (pkgs.lib.escape (pkgs.lib.stringToCharacters "\\ ';$`()|<>\t")) a);
      dotnet-sdk = pkgs.dotnet-sdk_7;
      version = "0.0.1";
    in {
      packages = {
        fetchDeps = let
          flags = [];
          runtimeIds = map (system: pkgs.dotnetCorePackages.systemToDotnetRid system) dotnet-sdk.meta.platforms;
        in
          pkgs.writeShellScript "fetch-${pname}-deps" (builtins.readFile (pkgs.substituteAll {
            src = ./fetchDeps.sh;
            pname = pname;
            binPath = pkgs.lib.makeBinPath [pkgs.coreutils dotnet-sdk (pkgs.nuget-to-nix.override {inherit dotnet-sdk;})];
            projectFiles = toString (pkgs.lib.toList projectFile);
            testProjectFiles = toString (pkgs.lib.toList testProjectFile);
            rids = pkgs.lib.concatStringsSep "\" \"" runtimeIds;
            packages = dotnet-sdk.packages;
            storeSrc = pkgs.srcOnly {
              src = ./.;
              pname = pname;
              version = version;
            };
          }));
        default = pkgs.buildDotnetModule {
          pname = pname;
          version = version;
          src = ./.;
          projectFile = projectFile;
          nugetDeps = ./deps.nix;
          doCheck = true;
          dotnet-sdk = pkgs.dotnet-sdk_7;
          dotnet-runtime = pkgs.dotnetCorePackages.runtime_7_0;
        };
      };
      devShell = pkgs.mkShell {
        buildInputs = with pkgs; [
          (with dotnetCorePackages;
            combinePackages [
              dotnet-sdk_7
              dotnetPackages.Nuget
            ])
        ];
        packages = [
          pkgs.alejandra
        ];
      };
    });
}
