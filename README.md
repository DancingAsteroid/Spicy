# Problem solving with Circuits
**English Below**

Spicy (Solve Problems in circuits yourself) ist eine VR-Lernanwendung zum Training von Problemlösekompetenzen. Mit Spicy können Lernende insbesondere Kompetenzen im Auswählen und Prüfen von Hypothesen im Rahmen der Fehlerdiagnostik elektrischer Schaltkreise erwerben. Spicy wurde im Rahmen einer Masterarbeit zu Spatial Presence beim Problemlösen in VR entwickelt.


Verweise auf unter Creative Commons veröffentlichter Assets, die in diesem Projekt benutzt wurden, finden sich in der Credits.txt

## Erstmaliges Setup des Unity Projekts
Spicy wurde mit der Unity-Version 2021.3.25f entwickelt. Beim ersten Starten des Projektes in Unity müssen noch einige Assets aus Pakten importiert werden. Unity sollte beim ersten Starten automatisch einen Knopf anzeigen, mit dem Daten für TextMeshPro automatisch importiert werden können. Zusätzlich müssen noch im Package Manager unter "XR Interaction Toolkit" bei "Samples" die "Starter Assets" und der "XR Device Simulator" importiert werden, damit alles fehlerfrei läuft (eventuell muss die Szene 1-2 mal neu gestartet werden). Zum Testen ohne VR-Ausrüstung muss natürlich der Device Simulator in der Hierarchy aktiviert werden.

## Nutzung
 Zur Standalone-Nutzung mit einem kompatiblen VR-Headset die Anwendung entsprechend in Unity kompilieren. Im Rahmen meiner Masterarbeit wurde die Anwendung unter Windows ausgeführt, wobei die Ausgabe über eine per Kabel angeschlossene Meta Quest 1 erfolgt ist. Für dieses Setup ist Spicy somit am meisten getestet. Für die Standalone-Nutzung mit einem VR-Headset müssen je nach Leistungsfähigkeit der Hardware ggf. die dynamischen Lichtquellen der Glühlampen und LEDs deaktiviert werden, um die Framerate hoch genug zu halten.

Derzeit befinden sich noch auditive Anweisungen in der Anwendung, die für die Studiendurchführung genutzt wurden. Zudem sind noch Fragebögen in der Anwendung eingebaut, die vor einer rein didaktischen Nutzung der Anwendung zu entfernen wären.

## Credits
Die Modelle im "Models"-Ordner wurden unter Creative Commons Lizenzen veröffentlicht, eine genaue Übersicht inklusive der jeweilige Autoren ist in der Credits.txt zu finden.
Als einen wesentlichen Baustein konnte bei der Erstellung von Spicy auf "[Project Faraday](https://github.com/Schackasawa/faraday)" von Darren Schack zurückgegriffen werden, einer VR-Schaltkreissimulation, die für die physikalische Simulation der Schaltkreise die offene Bibliothek [SpiceSharp](https://spicesharp.github.io/SpiceSharp/index.html) nutzt. Weitere Infos in der Credits.txt


# English
Spicy (Solve Problems in circuits yourself) is a VR learning application for training problem-solving skills. In particular, Spicy enables learners to acquire skills in selecting and testing hypotheses in the context of error diagnosis for electrical circuits. Spicy was developed as part of a German master's thesis on spatial presence in problem solving in VR. Therefore, the language of the application is also German, although a translation to English wouldn't be much work.


References to assets published under Creative Commons that were used in this project can be found in Credits.txt

## How to setup the project in Unity
Spicy was developed with Unity version 2021.3.25f. When starting the project in Unity for the first time, some assets still need to be imported from packages. Unity should automatically display a button that lets you import the data for TextMeshPro when you start the scene for the first time. In addition, the ‘Starter Assets’ and the ‘XR Device Simulator’ must be imported in the Package Manager. Select the Package Manager, go to ‘XR Interaction Toolkit’ and then to ‘Samples’. Click the Import-Button for the ‘Starter Assets’ and the ‘XR Device Simulator’.
Once you imported all these files, everything should run smoothly (the scene may have to be restarted 1-2 times).
If you want to test the application without VR equipment, the device simulator must be activated in the hierarchy first.


## Usage
 For standalone use with a compatible VR headset, compile the application accordingly in Unity. As part of my master's thesis, the application was run under Windows, with output via a Meta Quest 1 connected by cable. Spicy is therefore best tested for this setup. For standalone use with a VR headset, depending on the performance of the hardware, the dynamic light sources of the bulbs and LEDs may have to be deactivated in order to keep the frame rate high enough.

There are currently still audio files with instructions in the application that were used to conduct the study. In addition, there are still questionnaires built into the application that should be removed before the using the application purely for educational purposes.

## Credits
The models in the ‘Models’ folder were published under Creative Commons licenses, a detailed overview including the respective authors can be found in Credits.txt.
An essential building block in the creation of Spicy was [Project Faraday](https://github.com/Schackasawa/faraday) by Darren Schack, a VR circuit simulation which uses the open library [SpiceSharp](https://spicesharp.github.io/SpiceSharp/index.html) for the physical simulation of the circuits. Further information in the Credits.txt
