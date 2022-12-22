runHook preInstall
target="$out/lib/dotnet/@pname@"
mkdir -p "$target"
cp -rv @outputFiles@ "${outputFilesArray[@]}" "$target"
pushd "$out"
remove-duplicated-dlls.sh
popd
set -f
for dllPattern in @dllPatterns@ ${dllFilesArray[@]}
do
  set +f
  for dll in "$target"/$dllPattern
  do
    [ -f "$dll" ] || continue
    if pkg-config $(basename -s .dll "$dll")
    then
      echo "$dll already exported by a buildInputs, not re-exporting"
    else
      create-pkg-config-for-dll.sh "$out/lib/pkgconfig" "$dll"
    fi
  done
done
set -f
for exePattern in @exePattern@ ${exeFilesArray[@]}
do
  set +f
  for exe in "$target"/$exePattern
  do
    [ -f "$exe" ] || continue
    mkdir -p "$out"/bin
    commandName="$(basename -s .exe "$(echo "$exe" | tr "[A-Z]" "[a-z]")")"
    makeWrapper \
      "$exe" \
      "$out/bin/$commandName" \
      ${makeWrapperArgs}
  done
done
runHook postInstall
