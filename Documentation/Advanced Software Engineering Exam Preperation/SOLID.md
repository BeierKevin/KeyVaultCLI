# SOLID

## Einzelverantwortlichkeitsprinzip (SRP)

### Positives Beispiel:

Die Klasse **CreatePasswordCommand** ist ein gutes Beispiel für das **Einzelverantwortlichkeitsprinzip (SRP)**. Ihre 
einzige Verantwortung ist es, ein neues Passwort zu erstellen und dieses zu speichern. Sie hat nur eine Methode Execute, die die Erstellung des Passworts verwirklicht.

### Negatives Beispiel:

Einer der Kandidaten kann die **VaultService** Klasse sein. Sie ist verantwortlich für die Erstellung, Speicherung und Löschung von Vaults. Eine Möglichkeit, **SRP** hier zu implementieren, könnte sein, jede Funktion in eine eigene Klasse auszulagern und eine gemeinsame Schnittstelle zu erstellen, die von jeder Klasse implementiert wird.

## Offen-geschlossenes Prinzip (OCP)

### Positives Beispiel:

Die **ICommand** Schnittstelle und deren Implementierungen sind ein gutes Beispiel für das **Offene-/Geschlossene-Prinzip (OCP)**. Neue Befehle können hinzugefügt werden, indem einfach eine neue Klasse erstellt wird, die die Schnittstelle **ICommand** implementiert, ohne vorhandenen Code zu ändern.

### Negatives Beispiel:

Das Negative Beispiel ist auch die **VaultService** Klasse. Wenn wir eine neue Funktion hinzufügen müssen (wie z.B. 
das Aktualisieren des Vaults), müssen wir die Klasse direkt ändern. Eine mögliche Lösung könnte darin bestehen, die Verwendung des Strategy bzw. **Command-Patterns** zu vertiefen und jede Operation als einen einzelnen Befehl darzustellen.

## Liskovsches Substitutionsprinzip (LSP)

[//]: # (TODO: add UML-Diagram for LSP)

### Positives Beispiel:

Die Implementierungen der **ICommand** Interface wie **CreatePasswordCommand**, **DeletePasswordCommand**, usw. sind gute Beispiele für das **Liskovsche Substitutionsprinzip**. Jede dieser Klassen kann das **ICommand** Interface ersetzen, ohne dass das Verhalten des Systems beeinträchtigt wird.

### Negatives Beispiel:

In Ihrem Code konnte ich kein spezifisches Beispiel für eine Verletzung des Liskovschen Substitutionsprinzips finden. Das ist gut, denn eine Verletzung dieses Prinzips würde auf ein tiefgreifendes Designproblem hinweisen.
