## clanify - Analytics Client

Der Analytics Client ist eine Anwendung welche unter Windows installiert werden 
kann um Demo-Dateien f�r den eSport Shooter CS:GO auslesen und in eine Datenbank 
importieren zu k�nnen. Die in dieser Datenbank gespeicherten Informationen k�nnen 
dann verwendet werden um Statistiken und Analysen zu erstellen. Da das Auslesen 
dieser Demo-Dateien einige Ressourcen in Anspruch nimmt, wurde der Import �ber 
eine solche Anwendung gel�st.

Aktuell wird nur eine Verbindung zu einer MySQL-Datenbank unterst�tzt. Das
Script um die Tabellen in der Datenbank erstellen zu k�nnen, befindet sich [hier](https://github.com/clanify/clanify-analytics-sql/blob/master/database-tables.sql).

Weitere Scripte / SQL-Abfragen um Informationen aus den Daten zu ermitteln, befinden sich
im Projekt [clanify - Analytics SQL](https://github.com/clanify/clanify-analytics-sql).