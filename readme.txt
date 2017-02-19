A patch that allows FEZ v1.11 to be TASed

./FEZ_TAS
  Contains all of the auxillary files for running FEZ
  Rename FEZ.exe to FEZ.orig.exe
  Rename FezEngine.dll to FezEngine.orig.dll
  Place MonoMod.exe and Mono.Cecil.dll in here
    aquired from https://github.com/0x0ade/MonoMod
    last version tested was 1.17.xxx

./Mod_FEZ
  Contains the code to be injected into FEZ.exe

./Mod_FezEngine
  Contains the code to be injected into FezEngine.dll
