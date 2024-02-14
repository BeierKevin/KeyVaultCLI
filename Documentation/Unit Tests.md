# Unit Tests

## 10 Unit Tests

## Automatic Thorough Repeatable Independent Professional (ATRIP)

- ATRIP ist ein Akronym zur Bewertung von Tests. Es steht für:
- Automatic: Tests können automatisch ausgeführt werden, ohne menschliches Eingreifen.
- Thorough: Die Tests decken alle wichtigen Aspekte des zu testenden Codes ab.
- Repeatable: Die Tests liefern stets die gleichen Ergebnisse, wenn sie mehrmals mit denselben Voraussetzungen 
  ausgeführt werden.
- Independent: Tests sollten unabhängig voneinander ausführbar sein und nicht auf den Ergebnissen anderer Tests 
  basieren.
- Professional: Die Tests sollten in einer professionellen und verständlichen Art und Weise implementiert und 
  dokumentiert sein.

Ein Rein-Unit-Test sollte immer "ATRIP" sein.

- Automatic: Deine Tests laufen automatisch, da du sie innerhalb deines Test-Projekt implementierst und diese durch 
das Test-Framework ausgeführt werden können.
- Thorough: Die Tests decken verschiedene Use-Cases und Randbedingungen ab (z.B. leeres Passwort, falsches Passwort..)
  Hierfür könnte man schauen ob man 100% Code-Coverage erreicht hat, indem man jeden Codepfad mindestens einmal durchläuft. Das lässt sich idR über Tools in Rider messen.
- Repeatable: Deine Tests sind wiederholbar, weil sie immer das gleiche Ergebnis liefern, sofern die Codebasis gleich 
  bleibt.
- Independent: Deine Tests beeinflussen sich nicht gegenseitig, da sie unabhängig voneinander ausgeführt werden.

### ATRIP: Automatic

Die Tests wurden so entwickelt, dass sie vollständig automatisch ausgeführt werden können, ohne dass ein manueller Eingriff erforderlich ist. Dies wurde durch die Integration in die Entwicklungsumgebung JetBrains Rider erreicht. Dort wurde eine Testkonfiguration erstellt, die alle Tests im Projekt ausführt. So können die Tests mit nur einem Klick oder sogar automatisch bei jeder Kompilierung ausgeführt werden.

Die Tests in diesem Projekt nutzen die standardisierten `[TestClass]` und `[TestMethod]` Annotationen von MSTest. Dadurch erkennt die genutzte IDE Rider automatisch, welche Methoden als Tests ausgeführt werden sollen, wenn das zugehörige Testprojekt ausgeführt wird. Jedes Ausführen des Testprojekts, führt damit zur automatisierten Ausführung aller definierten Tests.

### ATRIP: Thorough

[//]: # (TODO: Code Coverage im Projekt analysieren und begründen)

### ATRIP: Professional

## Fakes und Mocks
