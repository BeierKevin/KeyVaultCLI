# Weitere Prinzipien

## Geringe Kopplung (Low Coupling)

### Positives Beispiel:

Die Klasse `VaultFactory` ist ein hervorragendes Beispiel für eine geringe Kopplung (Low Coupling). Sie ist verantwortlich für die Erstellung von `Vault`-Objekten, ist aber nur mit den Schnittstellen `IConsole`, `IVaultEncryptionService`, `IVaultFileService` und `IVaultPasswordGenerator` gekoppelt, nicht aber mit deren konkreten Implementierungen. Das heißt, wir können die konkreten Implementierungen ändern oder erweitern, ohne die `VaultFactory`-Klasse ändern zu müssen. Dies erleichtert die Wartung und Erweiterung des Codes, und fördert die Modularität und Austauschbarkeit der Komponenten.

#### weiteres, aber schon behandelt

Die Schnittstelle **ICommand** ist ein hervorragendes Beispiel für eine geringe Kopplung. Durch die Bereitstellung 
einer einfachen Schnittstelle (**Execute Methode**), die von vielen unterschiedlichen Befehlsklassen implementiert wird, ist **ICommand** nur schwach gekoppelt mit diesen Klassen. Die spezifischen Implementierungsdetails sind in den jeweiligen Befehlsklassen versteckt und **ICommand** muss nichts über sie wissen. Dies ermöglicht es, neue Befehle leicht hinzuzufügen oder bestehende zu ändern, ohne das **ICommand** Interface oder andere Teile des Systems zu beeinflussen.

### Polymorphismus (Polymorphism)

Im vorherigen Beispiel wurde Polymorphismus bereits durch die Nutzung von **ICommand** Interface gezeigt. Verschiedene Befehle wie **DeleteAllPasswordsCommand**, **CreatePasswordGenerateCommand**, **DeletePasswordCommand** usw., implementieren das **ICommand** Interface und ihre Spezifikationen werden durch die **Execute-Methode** realisiert. Das System kann diese Befehle polymorph behandeln, indem es sie als **ICommand** behandelt und die entsprechende **Execute-Methode** aufruft, ohne den spezifischen Befehlstyp zu kennen.

## Don't Repeat Yourself (DRY)

Leider kann ich ohne Zugriff auf Ihren Versionskontrollverlauf keine spezifischen Commits überprüfen, in denen Sie duplizierten Code/duplizierte Logik entfernt haben. Jedoch ist das Prinzip "Don't Repeat Yourself" (DRY) ein grundlegender Grundsatz in der Softwareentwicklung, der dazu dient, Wiederholungen zu vermeiden, indem gemeinsam genutzter Code in Funktionen, Methoden oder Klassen gekapselt wird. Durch die Einhaltung dieses Prinzips wird die Codebasis oft einfacher zu warten und zu erweitern.

Wenn Sie in der Vergangenheit duplizierten Code entfernt haben, dann haben Sie wahrscheinlich die Wartbarkeit Ihres Codes verbessert und das Risiko von Fehlern reduziert, da Änderungen nur an einer Stelle und nicht an vielen verschiedenen Orten vorgenommen werden müssen.

[//]: # (TODO: add commit example for duplicate code)