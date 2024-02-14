# Clean Architecture

## About the Project

Das Key Vault CLI-Projekt nutzt teilweise das Clean Architecture-Muster, um eine klare Trennung der Verantwortlichkeiten und die Einhaltung der Prinzipien des Domain Driven Design (DDD) sicherzustellen, um eine klare Trennung der Verantwortlichkeiten und eine wartbare Codebasis zu gewährleisten.

### Clean Architecture

Clean Architecture ist eine Design-Philosophie, die die Trennung der Verantwortlichkeiten und die Unabhängigkeit der Komponenten in Softwaresystemen priorisiert. Sie betont die Schichtung, wobei jede Schicht auf bestimmte Funktionalitäten fokussiert ist, und fördert Testbarkeit, Einfachheit und Anpassungsfähigkeit. Zu den wichtigsten Prinzipien gehören die Trennung der Geschäftslogik von externen Abhängigkeiten, die Sicherstellung, dass Abhängigkeiten nach innen zeigen, und die Priorisierung von Klarheit und Verständlichkeit.

### Domain Driven Design (DDD)

Domain-Driven Design (DDD) ist ein Ansatz für die Softwareentwicklung, der das Domänenmodell und die Geschäftslogik in den Mittelpunkt der Anwendung stellt. Er betont die Zusammenarbeit zwischen technischen und domänenspezifischen Experten, konzentriert sich auf das Domänenmodell und zielt darauf ab, ein gemeinsames Verständnis der Domäne und ihrer Anforderungen zu schaffen. Zu den wichtigsten Konzepten gehören begrenzte Kontexte, allgegenwärtige Sprache und domänenspezifische Designmuster.

## Analyzing the Dependency Rule

### Positive Example: Dependency Rule

Die Klasse **DeleteVaultCommand** ist ein gutes Beispiel für eine Einhaltung der Dependency-Rule. 
Sie ist Teil der Application-Schicht und implementiert das **ICommand** Interface aus der inneren Schicht (Domain). 
Diese Klasse ist abhängig von Interfaces der inneren Schichten wie **IVaultFactory** und **IConsole**, was der Regel 
entspricht, dass Abhängigkeiten immer in Richtung der zentralen Ebene zeigen sollten.

### Negative Example: Dependency Rule

In Ihrem Code habe ich kein offensichtliches Negativ-Beispiel für eine Verletzung der Dependency Rule gefunden.

## Analyzing the layers

**Application Schicht:** Die Klasse **CreatePasswordGenerateCommand** ist ein Beispiel für eine Klasse in der
**Application-Schicht**. Diese Klasse implementiert die Geschäftslogik, die notwendig ist, um ein neues Passwort zu generieren und hinzuzufügen. Sie ist abhängig von der **Domain-Schicht** (Interfaces **IVault** und **IConsole**), was der Struktur der Clean Architecture entspricht.

**Domain Schicht:** Die Klasse **IVault** (ein Interface) ist ein Beispiel für eine Klasse in der **Domain-Schicht**. Diese Klasse definiert die Business-Regeln und -Operationen, die auf den "Vault" angewendet werden können, einschließlich der Erstellung und Löschung von Passwörtern. Sie hat keine Abhängigkeiten zu den äußeren Schichten, was der Struktur der Clean Architecture entspricht.
