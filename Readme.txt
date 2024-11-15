
******** Costruzione del Progetto ******
-> Aprire il file MicroShop.sln

******** Una cosa Importante ********

Non so perché ma quando avvio il progetto da Visual Studio con Docker-compose 
automaticamente va a prendere il certificato da:
C:\Users\<utente>\AppData\Roaming\ASP.NET\Https

anche se dentro Docker-compose ho specificato indirizzo della cartella Https.
Quindi le prego di copiare il certificato da MicroShop\ASP.NET\Https a
C:\Users\<utente>\AppData\Roaming\ASP.NET\Https

********* Cos'è  e Come Funziona il progetto ********
È un ecommerce, ho seguito la traccia. Ci sono tre ruoli utente normale, supplier e admin.
Se per qualche motivo riceve 404 significa che non è autorizzato bisogna controllare 
con quale utente ha effettuato login.

Una volta avviato docker-compose bisogna aspettare anche se apre il primo 
micro servizio warehouse. Dopo qualche secondo si può andare a 
https://localhost:45492/

e registrasi come un utente normale. Dopo la registrazione questo utente può essere convertità 
a supplier. Per convertirlò deve fare login come Admin : admin@micro.it e password: Qwerty@123.
Per convertire utente precedentemente registrato bisogna andare all'indirizzo:
https://localhost:45292/swagger/index.html (Supply) e usare il metodo add supplier e indicare email.
Usando lo stesso microservizo ma come supplier (bisona fare login come supplier con lo stesso mail)
può aggiungere i prodotti c'è un metodo facile add random products. 
Adesso che ci sono prodotti si può fare ordine come admin usando tutte queste api.
Una volta fatto l'ordine supplier deve fare update status e mettere "delivered". Dopo questa 
chiamata i prodotti sarano disponibili nei tutti microservizi. Poi si possono fare ordine da:
https://localhost:45492/

Ci sono 5 microservizi: 
warehouse.api https://localhost:45092/
payment.api https://localhost:45192/
supply.api https://localhost:45292/
order.api https://localhost:45392/
microshop.webapp https://localhost:45492/


