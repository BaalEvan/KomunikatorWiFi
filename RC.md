#RedMine Client

##Moduły

###Lista pracowników
 - Zawiera listę wszystkich pracowników firmy pobieraną z Redmine.
 - Każdy pracownik który posiada uzupełniony profil Redmine o Nazwe Użytkownika z Telegram'u posiada przycisk do szybkiego nawiązania rozmowy *(otwiera zainstalowaną w systemie aplikacje Telegram będącą obecnie oficjalną aplikacją komunikacyjną w naszej firmie)*
 - Na podstawie użycia aplikacji przez użytkownika *(TimeTracker)* po uruchomieniu nowego zadania/zagadnienia klient aktualizuje obecny stan pracownika *( OnLine / Offline )* bieżące zadanie *( Widoczne na liście pracowników )* oraz jego priorytet *( Kolorowe obramowanie wokół avatara pracownika obsługujące 5 różnych priorytetów)*
 - Dane z serwera są pobierane w korzystając z nowego wątku dzięki czemu podczas aktualizacji danych *( dane są sprawdzane pod kątem zmian nie tylko wgrywane nowe )* aplikacja pozostaje responsywna a lista pracowników nie zmienia swojej pozycji *( nie scroll'uje się automatycznie )*
 - Aktualizacja jest oparta o GlobalTimer i wykonuje się cyklicznie co określoną ilość minut *( w przyszłości możliwość konfiguracji )* 
 - Każda zmiana stanu pracownika wywołuje notyfikacje *( NotificationCenter )*
 - Mechanizm pobierania Avatarów oparta o Gravatar z dodanym Avatarem firmowym *( mechanizm jest w pełni kompatybilny z Redmine )*
###Opcje
 - Zawierają podstawowe ustawienia aplikacji takie jak
	- Klucz dostępu do Redmine
	- Włącznik/Wyłącznik Snap To Screen
	- Włącznik/Wyłącznik Bubble Icon
	- Włącznik/Wyłącznik Pomodoro Clock
	- Opcję wyboru z której strony ma być przycisk TAK w alertach *( na życzenie pracowników )*
	- Docelowo także:
		- Logowanie do Clienta Poczty
		- Ustawianie częstotliwości sprawdzania zmian na serwerze *( GlobalTimer )*
		- Ustawienia długości trwania przerwy dla Pomodoro Clock
###SnapToScreen
 - Automatyczne przyciąganie okna do krawędzi ekranu
 - Wsparcie dla wielu monitorów
 - Wsparcie dla konfiguracji z bocznym paskiem zadań
###Lista Zadań/Zagadnień
 - Zawiera pogrupowaną listę wszystkich zadań przypisanych do naszej osoby
 - Pozwala na "Zwinięcie" danego projektu, aby zadania zajmowały mniej miejsca
 - Pozwala na Filtrowanie po projekcie dzięki czemu wyświetlane są tylko te zadania które zostały dodane do konkretnego projektu
 - Wszystkie zadania cały czas pozostają w pamięci komputera więc zmiana projektu nie wymaga komunikacji z Redmine
 - Pozwala na wyszukiwanie konkretnych zadań po 3 różnych kryteriach 
	- Nazwa Zadania
	- ID Zadania *( Redmine posiada jawny i często wykorzystywany przy referencjach numer dla każdego Zagadnienia )*
	- Autorze zadania
 - Dane z serwera są pobierane w korzystając z nowego wątku [...] *( dane są sprawdzane pod kątem zmian nie tylko wgrywane nowe )* aplikacja [...] *( nie scroll'uje się automatycznie )* tylko dodaje lub kasuje zmienione zadania
 - Aktualizacja [...] wykonuje się cyklicznie co określoną ilość minut ...
 - Każda zmiana zadania *( przypisanie nowego, zamknięcie, dodanie jako osoby wykonującej, zmiana osoby przypisanej )* wywołuje notyfikacje *( NotificationCenter )*
###TimeTracker
 - Zawiera listę 5 ostatnio wybranych z listy zadań pozycji, przycisk uruchamiania odliczania, pole wyboru aktywności, licznik czasu, pole z aktualną godziną, pole przeznaczone na komentarz oraz odnośnik do PersonalTaskList.
 - Uruchomienie licznika bądź zmiana zadania podczas pracy licznika powoduje zaktualizowanie stanu pracownika na liście pracowników
 - Treść zadania jak i priorytet są pobiera automatycznie
 - Lista aktywności jest pobierana z Redmine i może być zmieniona w każdej chwili.
 - Jeżeli zmieniona zostanie godzina w polu z aktualną godziną czas będzie liczony od godziny podanej przez użytkownika. Jeżeli godzina była nieprawidłowa bądź większa niż ta obecnie zostanie ustawiona obecna.
 - Skorzytanie z PersonalTaskList uzupełni automatycznie komentarz do obecnego zadania *( Wpis z aktywnością, Zadaniem czasem trwania i komentarzem jest zapisany w Dzienniku na Redmine )*
###TrayIcon
 - Na życzenie krzyśka wprowadzona została ikona w Trayu zawiera ona aktualnie tylko informacje o bieżącej sesji Pomodoro Timera *( Informacje czy pracownik ma przerwę ile pozostało mu czasu do końca lub do rozpoczęcia przerwy )*
 - Może zostać dowolnie rozbudowana
 - Wcześniej służyła jako wywoływacz do Notyfikacji *( w windows 10 dawne dymki z Traya zostały zamienione na notyfikacje zgodne z UI systemu )*
###NotificationCenter
 - Zaimplementowane w 2 Trybach - Windows 10 oraz Windows 8 aktualnie wszystkie standardowe powiadomienia opierają się na schemacie Windows 8 dla zachowania wstecznej kompatybilności, jednakże nic nie stoi na przeszkodzie by w systemie Win10 korzystać z obu trybów.
 - Standardowe kompatybilne z Windows 8 notyfikacje składają się z: Ikony, Tekstu tytułowego oraz do 3 linii tekstu poniżej dzięki konstruktorowi opartemu o tablicę możemy zamieścić tam dowolny tekst oraz grafikę z dysku. Aby dało się wyświetlać avatar pracownika przy informacji o zmianie statusu należy pobrać go dodatkowo na dysk *( można zautomatyzować jeśli ten pomysł będzie wdrożony )*
 - Notyfikacje Generyczne Windows 10 pozwalają dodatkowo korzystać z Interaktywnych powiadomień *( posiadających przyciski akcji )* dzięki czemu potwierdzenie wysłania wpisu na serwer może odbywać się z UI notyfikacji. Podobnie wygląda sytuacja ze zmianą postępu prac nad danym zagadnieniem *( limit 5 elementów na ComboBoxie, ale może to zostać rozwiązane poprzez *( current Progress + 4 wyższe )* wówczas jedynie małe zadania które wykonuje się za 1 razem od 0 do 100% wymagają innego komponentu. 
###MailClient
 - Jest to moduł wbudowany bez własnego interfejsu na chwilę obecną.
 - Aktualizacja [...] wykonuje się cyklicznie co określoną ilość minut ...
 - Pobiera wiadomości z naszego serwera i informuje notyfikacją.
 - Zapisuje raz pobrane wiadomości, aby nie wyświetlać kolejny raz tej samej wiadomości
 - Ma cel informacyjny zbudowanie na jej podstawie całego clienta jest możliwe, ale niekoniecznie ma sens. jeśli można skorzystać z dedykowanych temu rozwiązań *( Thunderbird, Outlook )*
###BubbleIcon
 - Jest to pływająca ikonka będąca reprezentacją naszej aplikacji w trybie zminimalizowanym oprócz możliwości dowolnego przestawiania i otwierania aplikacji do pełnego okna pełni również rolę informującą dla Pomodoro Clock'a informując o pozostałym czasie do końca bądź początku przerwy.
###PomodoroClock
 - Jest to moduł wbudowany bez własnego dedykowanego interfejsu ale zintegrowany z innymi modułami.
 - Jest to implementacja techniki zarządzania czasem [ref.Pomodoro](http://produktywnie.pl/1998/technika-pomodoro-najprostsza-metoda-zarzadzania-czasem/)
 - Działa w następujący sposób:
	 - W momencie uruchomienia zadania startuje wraz z licznikiem głównym po 25 minutach pracy pracownik dostaje informacje o 5 minutowej przerwie którą może poświęcić na wyjście do sklepu bądź na papierosa, albo na siedzienie na facebooku jeśli lubi. Pozostały czas powinien być poświęcony na pracę.
	 - Po każdej przerwie licznik się zeruje i ponownie zostaje ustawione 25 minut pracy
 - Możliwe rozszerzenia
	- 5 minut przed końcem pracy pracownik jest proszony o podsumowanie danego segmentu pracy dzięki czemu możliwe będzie dokładniejsze opisywanie pracy pracowników
	- Dodanie informacji o stanie zegara na ikonie na pasku zadań 
 - Dodatkowo: Easter Egg pracownicy posiadający sprzęt firmy Logitech z możliwością kontrolowania jasności podświetlenia zostaną poinformowani o zmianie trybu zegara *( Praca / Przerwa )* poprzez krótką pulsującą notyfikację świetlną *( Czerwony Praca / Zielony Przerwa dla Urządzeń wspierających RGB )* 
###PersonalTaskList
- Zawiera listę zadań z informacją o zaznaczeniu *( pozwala zapamiętać czy dane zadanie było wykonywane poprzednim razem i automatycznie dodać go ponownie do komentarza )* oraz możliwością usuwania
- Zawiera pole tekstowe służące do wprowadzania nowych zadań z możliwością dodawania za pomocą klawiatury
- Po zaznaczeniu zadań i kliknięciu przycisku zapisz zadania są wpisywane do komentarza oraz jeżeli został przekroczony zakres długości pola **( 255 znaków wymóg Redmine )** sygnalizuje ten fakt zmieniając kolor tekstu na czerwony
###GlobalTimer
- Jest to moduł odpowiadający za wszystkie prace wykonywane w tle na dodatkowych wątkach, zadania są do niego przypinane więc integracja nowych zadań polega na dodaniu w ramach kodu głównego programu jednej linijki z referencją do funkcji oraz czasu co ile ma się wykonywać zadanie.
- Jeżeli wymagana jest praca na wątku aplikacji *(UI)* wówczas może zostać dodane zadanie na określonym wątku.
- Wywołuje on zdarzenia co określoną ilość jednostek czasu *( testowo 10sek docelowo minuta )* jeżeli w określonym czasie znajduje się jakieś polecenie do wykonania wywołuje je.
###Dodatkowo:
- Aplikacja jest przystosowana do wspierania wielu języków. W wersji **v1.0.1** będącej pierwszym oficjalnym wydaniem zamieszczonym na repozytorium funkcjonowała w 2 językach Polskim i Angielskim ze względu na zmiany od tamtego czasu na chwilę obecną na język Angielski przetłumaczone jest około 80% - *Gdybyśmy chcieli sprzedać.*
