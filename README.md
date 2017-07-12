## clanify - Analytics Client

Der Analytics Client ist eine Anwendung welche unter Windows installiert werden 
kann um Demo-Dateien für den eSport Shooter CS:GO auslesen und in eine Datenbank 
importieren zu können. Die in dieser Datenbank gespeicherten Informationen können 
dann verwendet werden um Statistiken und Analysen zu erstellen. Da das Auslesen 
dieser Demo-Dateien einige Ressourcen in Anspruch nimmt, wurde der Import über 
eine solche Anwendung gelöst.

Aktuell wird nur eine Verbindung zu einer MySQL-Datenbank unterstützt. Das
Script um die Tabellen in der Datenbank erstellen zu können, befindet sich [hier](https://github.com/clanify/clanify-analytics-sql/blob/master/database-tables.sql).

Weitere Scripte / SQL-Abfragen um Informationen aus den Daten zu ermitteln, befinden sich
im Projekt [clanify - Analytics SQL](https://github.com/clanify/clanify-analytics-sql).