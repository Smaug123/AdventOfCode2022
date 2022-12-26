#!/bin/bash
runHook preInstall
target="${out:?}/lib/dotnet/@pname@"
mkdir -p "$target"
cp -rv @outputFiles@ AdventOfCode2022.App/bin/Release/net7.0/publish/* "$target"
pushd "$out" || exit 1
remove-duplicated-dlls.sh
popd || exit 2
set -f
exe="$target/AdventOfCode2022.App"
[ -f "$exe" ] || exit 3
mkdir -p "$out"/bin
commandName="$(basename -s .exe "$(echo "$exe" | tr "[:upper:]" "[:lower:]")")"
makeWrapper \
  "$exe" \
  "$out/bin/$commandName"

runHook postInstall
