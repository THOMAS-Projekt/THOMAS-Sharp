# THOMAS-Sharp

C#-Rewrite des Quellcodes von THOMAS

__Optimiert f�r:__ .Net-Framework 4.5.2 (oder �lter)

## Entwicklung auf Windows
F�r die Entwicklung auf Windows empfiehlt sich die Verwendung der Visual Studio IDE, mit der sich die `THOMAS-Sharp.sln`-Datei im Projekt-Hauptverzeichnis �ffnen l�sst.
Damit anschlie�end ein Kompilieren m�glich ist, sollte �ber _Extras_ -> _NuGet-Paketmanager_ -> _NuGet-Pakete f�r Projektmappe verwalten_ der Paket-Manager ge�ffnet werden und in der auftauchenden Meldung am oberen Fensterrand die Paketwiderherstellung gestartet werden. Alles weitere erfolgt automatisch und anschlie�end sollte sich das Projekt kompilieren lassen.

## Ausf�hren auf Ubuntu
### Mono installieren
Da die Mono-Versionen in den Paketquellen meist minimal veraltet sind, empfiehlt es sich die [offizielle Mono-Paketquelle](http://www.mono-project.com/docs/getting-started/install/linux/#debian-ubuntu-and-derivatives) zu verwenden und damit die aktuellste Mono-Version zu installieren.

### Kompilieren
Zun�chst sollte auch hier wieder sichergestellt werden, dass alle Pakete verf�gbar sind. Dazu einfach `nuget restore` im Projekt-Hauptverzeichnis ausf�hren.
Anschlie�end kann in das Projektverzeichnis gewechselt werden, das man kompilieren m�chte und f�hrt dann beispielsweise `xbuild THOMAS-Server.csproj` aus. Nach erfolgreichem Abschluss der Kompilierung liegen die Bin�rdateien im Unterverzeichnis `/bin/Debug`. Diese k�nnen dann wie bei Linux gewohnt ausgef�hrt werden.