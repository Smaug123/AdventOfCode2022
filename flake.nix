{
  description = "Advent of Code 2022";

  inputs = {
    flake-utils.url = "github:numtide/flake-utils";
    nixpkgs.url = "nixpkgs/nixpkgs-unstable";
  };

  outputs = {
    nixpkgs,
    flake-utils,
    ...
  }:
    flake-utils.lib.eachDefaultSystem (system: let
      pkgs = nixpkgs.legacyPackages.${system};
      projectFile = "./AdventOfCode2022.App/AdventOfCode2022.App.fsproj";
      testProjectFile = "./AdventOfCode2022.Test/AdventOfCode2022.Test.fsproj";
      pname = "AdventOfCode2022";
      dotnet-sdk = pkgs.dotnetCorePackages.sdk_9_0;
      dotnet-runtime = pkgs.dotnetCorePackages.runtime_9_0;
      version = "0.0.1";
      dotnetTool = toolName: toolVersion: hash:
        pkgs.stdenvNoCC.mkDerivation rec {
          name = toolName;
          version = toolVersion;
          nativeBuildInputs = [pkgs.makeWrapper];
          src = pkgs.fetchNuGet {
            pname = name;
            version = version;
            hash = hash;
            installPhase = ''mkdir -p $out/bin && cp -r tools/*/any/* $out/bin'';
          };
          installPhase = ''
            runHook preInstall
            mkdir -p "$out/lib"
            cp -r ./bin/* "$out/lib"
            makeWrapper "${dotnet-runtime}/bin/dotnet" "$out/bin/${name}" --add-flags "$out/lib/${name}.dll"
            runHook postInstall
          '';
        };
    in {
      packages = let
        deps = builtins.fromJSON (builtins.readFile ./nix/deps.json);
      in {
        fantomas = dotnetTool "fantomas" (builtins.fromJSON (builtins.readFile ./.config/dotnet-tools.json)).tools.fantomas.version (builtins.head (builtins.filter (elem: elem.pname == "fantomas") deps)).hash;
        default = pkgs.buildDotnetModule {
          pname = pname;
          version = version;
          src = ./.;
          projectFile = projectFile;
          testProjectFile = testProjectFile;
          nugetDeps = ./nix/deps.json; # `nix build .#default.fetch-deps && ./result nix/deps.json`
          doCheck = true;
          dotnet-sdk = dotnet-sdk;
          dotnet-runtime = dotnet-runtime;
        };
      };
      devShell = pkgs.mkShell {
        buildInputs = [dotnet-sdk];
        packages = [
          pkgs.alejandra
          pkgs.nodePackages.markdown-link-check
          pkgs.shellcheck
        ];
      };
    });
}
