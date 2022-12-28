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
      dotnet-runtime = pkgs.dotnetCorePackages.runtime_7_0;
      version = "0.0.1";
    in {
      packages = {
        fantomas = pkgs.stdenvNoCC.mkDerivation rec {
          name = "fantomas";
          version = "5.2.0-alpha-008";
          nativeBuildInputs = [ pkgs.makeWrapper ];
          src = pkgs.fetchNuGet {
            pname = name;
            version = version;
            sha256 = "sha256-1egphbWXTjs2I5aFaWibFDKgu3llP1o32o1X5vab6v4=";
            installPhase = ''mkdir -p $out/bin && cp -r tools/net6.0/any/* $out/bin'';
          };
          installPhase = ''
            runHook preInstall
            mkdir -p "$out/lib"
            cp -r ./bin/* "$out/lib"
            makeWrapper "${dotnet-runtime}/bin/dotnet" "$out/bin/fantomas" --add-flags "$out/lib/fantomas.dll"
            runHook postInstall
          '';
        };
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
          dotnet-sdk = dotnet-sdk;
          dotnet-runtime = dotnet-runtime;
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
