# Domain Driven Design

## Ubiquitous Language

1. Befehl (**Command**): Bei der Implementierung des Command Pattern bezeichnet ein "Befehl" eine Aktion, die auf eine 
bestimmte Weise ausgeführt werden soll. In Ihrem Fall sind Befehle spezifische Klassen wie **DeleteVaultCommand**, **CreatePasswordCommand** usw., die eine **Execute-Methode** implementieren.
2. Gewölbe (**Vault**): In Ihrem Kontext scheint ein "Vault" einen sicheren Speicher für Passwörter zu bezeichnen. Es ist 
   ein bedeutender Teil Ihrer Domänensprache und scheint Anwendungsfall-spezifische Methoden wie AddPasswordEntry, DeletePasswordEntry usw. bereitzustellen.
3. Master-Passwort (**Master Password**): Ein "Master-Passwort" scheint ein einzigartiges Passwort zu sein, dass zum 
   Erstellen bzw. Öffnen eines "Vaults" verwendet wird.
4. Passworteintrag (**Password Entry**): Ein "Passwort-Eintrag" scheint ein Datensatz mit dem Service-Namen, dem 
   Kontonamen und dem Passwort zu sein, der in einem "Vault" gespeichert ist.

## Repositories

In Ihrem Code scheint es kein Repository zu geben. Das liegt höchstwahrscheinlich daran, dass Sie eine sehr spezifische Anwendungslogik haben, die darin besteht, eine sichere Speicherung für Passwörter zu erzeugen und diese zu manipulieren. Ein Repository-Muster wird oft verwendet, wenn man mit einer Datenbank interagieren muss, was in Ihrem Fall nicht zutrifft. Sie haben Dienstleistungen wie **IConsole** und **IVault**, die direkte Interaktion mit dem User oder dem Sicherheitssystem ermöglichen.

## Aggregates

Hier scheint Ihr "Vault" als Aggregate zu dienen. Es handelt sich um eine Einheit von konsistentem Zustand, die durch IVault repräsentiert wird und Methoden zum Hinzufügen, Löschen und Abrufen von Passwörtern bietet, wobei die Konsistenz der Daten (wie korrektes Format und Gültigkeit der Eingaben) erhalten bleibt. Der Zustand des "Vault" ist durch die Menge der "Passwort-Einträge" definiert, die darin gespeichert sind.

## Entities

In Ihrer Domäne scheint das **Vault** eine Entity zu sein. Es hat eine deutliche Identität (durch das Master-Passwort, 
das zum Erstellen oder Öffnen des "Vaults" verwendet wird) und seinen Zustand, der durch die Liste der "Password Entries" repräsentiert wird, die darin gespeichert sind.

## Value Objects

In Ihrem Fall sind "Password Entries" gute Kandidaten für Value Objects. Jeder **PasswordEntry** hat keinen besonderen eigenen Identität, außer der Information, die er beinhaltet (Service-Name, Kontoname und Passwort). Daher haben zwei "Password Entries" mit identischen Informationen den gleichen Wert und können interchangeably verwendet werden. Value Objects sind unveränderlich in Ihrem Modell und jeder Versuch, einen "Password Entry" zu ändern, würde tatsächlich zu einem neuen "Password Entry" führen.