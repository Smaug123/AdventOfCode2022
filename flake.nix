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
        default = pkgs.buildDotnetPackage {
          pname = pname;
          version = "0.0.1";
          src = ./.;
          projectFile = projectFile;
          buildInputs = [
            pkgs.dotnet-sdk_7
            # unit tests
            pkgs.dotnetPackages.NUnit
            pkgs.dotnetPackages.NUnitRunners
          ];
          outputFiles = outputFiles;
          exeFiles = ["AdventOfCode2022.App"];
          nativeBuildInputs = [
            pkgs.pkg-config
          ];
          buildPhase = ''runHook preBuild && dotnet publish --configuration Release ${projectFile} && runHook postBuild'';
          doCheck = true;
          checkPhase = ''runHook preCheck && dotnet test --configuration Debug && dotnet test --configuration Release && runHook postCheck'';
          installPhase = builtins.readFile (pkgs.substituteAll {
            src = ./install.sh;
            outputFiles = arrayToShell outputFiles;
            pname = pname;
            dllPatterns = arrayToShell ["*.dll"];
            dotnet = pkgs.dotnet-sdk_7;
            exePattern = arrayToShell ["AdventOfCode2022.App"];
          });
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
