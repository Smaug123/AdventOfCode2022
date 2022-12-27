{
  description = "Advent of Code 2022";

  inputs = {
    flake-utils.url = "github:numtide/flake-utils";
    nixpkgs.url = "nixpkgs/nixpkgs-unstable";
    alejandra.url = "github:kamadorueda/alejandra/3.0.0";
    alejandra.inputs.nixpkgs.follows = "nixpkgs";
  };

  outputs = {
    self,
    nixpkgs,
    alejandra,
    flake-utils,
    ...
  }:
    flake-utils.lib.eachDefaultSystem (system: let
      pkgs = nixpkgs.legacyPackages.${system};
      projectFile = "./AdventOfCode2022.App/AdventOfCode2022.App.fsproj";
      pname = "AdventOfCode2022";
      outputFiles = [""];
      arrayToShell = a: toString (map (pkgs.lib.escape (pkgs.lib.stringToCharacters "\\ ';$`()|<>\t")) a);
    in {
      packages = {
        default = pkgs.buildDotnetModule {
          pname = pname;
          version = "0.0.1";
          src = ./.;
          projectFile = projectFile;
          nugetDeps = ./nuget.nix;
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
          alejandra.defaultPackage.${system}
        ];
      };
    });
}
