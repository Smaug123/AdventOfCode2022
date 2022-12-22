{
  description = "Advent of Code 2022";

  inputs = {
    flake-utils.url = "github:numtide/flake-utils";
    nixpkgs.url = "nixpkgs/nixpkgs-unstable";
  };

  outputs = { self, nixpkgs, flake-utils, ... }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = nixpkgs.legacyPackages.${system};
        projectFile = "./AdventOfCode2022.App/AdventOfCode2022.App.fsproj";
      in {
        packages = { default = pkgs.buildDotnetPackage {
        pname = "AdventOfCode2022"; version = "0.0.1"; src = ./.; projectFile = projectFile;
        buildInputs = [
            # unit tests
            pkgs.dotnetPackages.NUnit
            pkgs.dotnetPackages.NUnitRunners
          ];
          nativeBuildInputs = [
              pkgs.pkg-config
            ];
            buildPhase = ''
                runHook preBuild
                msbuild /p:Configuration=Release ${projectFile}
                runHook postBuild
              '';
        }; };
        devShell = pkgs.mkShell {
          buildInputs = with pkgs; [
            (with dotnetCorePackages; combinePackages [
              dotnet-sdk_7
              dotnetPackages.Nuget
              alejandra
            ])
          ];
        };
      });
}
