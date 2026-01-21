1) Hva betyr mønsteret `Handle(state, command, now) -> Result` med egne ord?
Håndtere en state, om den er akseptert. Returnere et resultat basert på handlingen. 
2) Hva er en `Outcome` og hvorfor har vi den?
Hvis vi ikke hadde hatt `Outcome` så hadde det blitt noe I/O i kjernen. Den er såpass modulær at den kan gi informasjon om store deler i applikasjonen.
3) Hva betyr det at kjernen er "uten I/O"?
Den har bare logikk innenfor selve programmet, uten å koble til filer/dependencies utenifra. Man eliminerer utforutsigbarhet. 
4) Hva måtte dere slå opp for å få dette til?
AI, noe dokumentasjon fra Microsoft, litt GeeksForGeeks, litt W3Schools. Litt om Assert for Unit Testing, interface for records, Søkt opp struct, record. Litt på record struct. Spør gjerne om record struct
5) Hva tror dere dere må lære mer om før vi bygger ekte MVP?
Record struct og record (default constructor) - men vi lærer når vi trenger det for det meste.
6) Hva er forskjellen på `class`, `record`, `struct` og `record struct` (kort)?
Default constructors
Noe implementasjon og features
Reference type (class, record), value type (struct, record struct)
7) Hva fant dere ut om default constructor i de fire tilfellene?
