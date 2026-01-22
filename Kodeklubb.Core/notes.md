1) **Hva betyr mønsteret `Handle(state, command, now) -> Result` med egne ord?**

Håndtere en state, om den er akseptert. Returnere et resultat basert på handlingen. Ansvaret til funksjonen er å gi et resultat utifra en handling, ikke noe I/O, bare business logic. 

2) **Hva er en `Outcome` og hvorfor har vi den?**

Hvis vi ikke hadde hatt `Outcome` så hadde det blitt noe I/O i kjernen. Den er såpass modulær at den kan gi informasjon om store deler i applikasjonen. Ansvaret her er å bare gi tilbakemelding på hvordan transaksjonen gikk.

3) **Hva betyr det at kjernen er "uten I/O"?**

Den har bare logikk innenfor selve programmet, uten å koble til filer/dependencies utenifra. Man eliminerer utforutsigbarhet.

4) **Hva måtte dere slå opp for å få dette til?**

AI, noe dokumentasjon fra Microsoft, litt GeeksForGeeks, litt W3Schools. Litt om Assert for Unit Testing, interface for records, Søkt opp struct, record. Litt på record struct.

5) **Hva tror dere dere må lære mer om før vi bygger ekte MVP?**

Record struct og record (default constructor) - men vi lærer når vi trenger det for det meste. "Kodestil" - GitHub Actions, workflow-verktøy som kan *lette trykket*. Default constructors 

6) **Hva er forskjellen på `class`, `record`, `struct` og `record struct` (kort)?**

Alle av de har en tom default constructor med mindre du spesifiserer - men for record, struct og record struct så er det common practice å bruke f.eks. primary constructor for å eliminere mutability. 
Record har litt mer features, som f.eks. en innebygd ToString som gjør det lett å sjekke i konsollet.
Class og record er reference type, struct og record struct er value type.
Alt som har record foran seg har funksjonalitet som gjør det lettere å praktisere immutability. 

7) **Hva fant dere ut om default constructor i de fire tilfellene?**

De er ganske samma, det blir det du gjør det til. Primary constructor i class og record gjør "init",
men du kan sette "set" - men det er en rule of thumb å praktisere immutability om det er målet. 
Struct har alltid en tom constructor.