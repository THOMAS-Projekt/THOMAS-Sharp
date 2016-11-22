# THOMAS-Sharp

C#-Rewrite des Quellcodes von THOMAS

__Optimiert für:__ .Net-Framework 4.5.2 (oder älter)

## Entwicklung auf Windows
Für die Entwicklung auf Windows empfiehlt sich die Verwendung der Visual Studio IDE, mit der sich die `THOMAS-Sharp.sln`-Datei im Projekt-Hauptverzeichnis öffnen lässt.
Damit anschließend ein Kompilieren möglich ist, sollte über _Extras_ -> _NuGet-Paketmanager_ -> _NuGet-Pakete für Projektmappe verwalten_ der Paket-Manager geöffnet werden und in der auftauchenden Meldung am oberen Fensterrand die Paketwiderherstellung gestartet werden. Alles weitere erfolgt automatisch und anschließend sollte sich das Projekt kompilieren lassen.

## Ausführen auf Ubuntu
### Mono installieren
Da die Mono-Versionen in den Paketquellen meist minimal veraltet sind, empfiehlt es sich die [offizielle Mono-Paketquelle](http://www.mono-project.com/docs/getting-started/install/linux/#debian-ubuntu-and-derivatives) zu verwenden und damit die aktuellste Mono-Version zu installieren.

### Kompilieren
Zunächst sollte auch hier wieder sichergestellt werden, dass alle Pakete verfügbar sind. Dazu einfach `nuget restore` im Projekt-Hauptverzeichnis ausführen.
Anschließend kann in das Projektverzeichnis gewechselt werden, das man kompilieren möchte und führt dann beispielsweise `xbuild THOMAS-Server.csproj` aus. Nach erfolgreichem Abschluss der Kompilierung liegen die Binärdateien im Unterverzeichnis `/bin/Debug`. Diese können dann wie bei Linux gewohnt ausgeführt werden.