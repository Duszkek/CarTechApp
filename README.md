Okay This is a monster that was created in like 4 work days after I passed my exams and done all my studies related work.
The readme won't be as nice as previously staded, a lot of tutorials were watched in order to make it usable and well, work.

First and foremost:
Model View ViewModel - From what I understand it's a pattern that seperates backed from frontend.
I have only one model - Car - because that's the only one that I would be using.
View - while internet says that its the structure, layout etc. I saw it more like my xaml files that could contain some very 
basic logic - but mostly they were connected to my viewmodels.
ViewModel - Contains logic and binds model and view together.

This is not very... Intuitive at first, and probably still is not very intuitive for me but I hope that General Idea was reflected inside my spaghetti code.

In my model folder there is a model of CarProp (well, since it doesn't contain all Car data (there is no ID) and it contains some Technical Exam table data I named
it Prop + there was a different idea of managing my variables but then I realised it would't work that way.

View folder contains front-end information for my pages. NearbyPage (at first I wanted to make a nice page that slides from the right side
after clicking on a Car in the List), now it is just a list and I'm a little bit out of time and CarDetailsPage that contains Car specific information.
Their xaml.cs files contains mostly binding to their viewmodels.

ViewModel folder contains basically all the logic behind each page:
MainView model is just a simple one:
Contains variables that are bound to the xaml file, so that user can enter a string of characters and using radio buttons check search type - that defines
the type of the string (if it is a registration number, model or brand of the car).
The button also goes to the next page pushing neccessary information into the NearbyPage.

NearbyPage view model:
There are some dependecies like INotifyPropertyChanged that I found could be neccessary since some information on the page change after it is loaded.
This viewmodel constructior also set up variables that will contain data pulled from the database and they will be saved as ObservableCollection<T>.
Refresh List Task Refreshes the List (pulles down new data from the database using getData() function from my service MicroserviceAPI.cs) and GoToDetails Task is
responsible for going into the next page with chosen data (also It takes the data that was used to search for this specific set of cars in the first place)
I also tried to use ActivityIndicator but failed miserably.
  
Details view model:
Some things are straight up copied from NearbyPage view model. It contains some datatypes and creates some commands to make button works. Here I was mainly
happy that my Car Details could be displayed on my screen after 8 hours of fixing problems. It contains mainly saveDateCMD command that sets up the button
to run saveDate function that would, well, save the Date and if the car passed the check up into the database.
If I could spend here some more time, or read some more about xamarin it would be great. For now after pressing this button it will create new nearbyPage on top
of the page's stack. I tried to push it before 2 pages and then delete top two but it had problems - app crashed after tapping car on the nearbyPage list.


Folder Services contains Interface IMicroservice and class Microservice that contain the code that focuses on connectivity with the database.
Function getData() gets list of cats from the database via previously set up data. If the Search Bar was empty a full list is returned.
To do it I had to use HttpClient class and GetAsync() method. Here I also spend 8 hours trying to fix a bug in the code:
So the HttpClient was set up, but in the next line (it still contained the data) but I was getting error that it is null.
It was a big pain to fix because I never really used any of async methods and it's basically my first xamarin code.
Later I found out that it is forbidden to use async methods inside page's constructor. And it still didn't work but thats actually my fault, but at
least I could see the problem in the exception.
Problem was that while on my PC my microservice on localhost worked perfectly, the connection didn't worked for mobile phone - I mean it a localhost so it was purely
my fault (overtaken by happines that the HttpClient was finally properly set up). If I could change one thing:
- HttpClient should be set up only once for the whole time the app is working.
So after this part of the code is done, the response is saved as string and then as Json and returned to NearbyViewModel and setup as ObservableCollection<CarProp> to
finally be seen on the list page.
  
SaveData method is simply the same but with few changes ( uses PostAsync() method and my car had to be serialized ). It also set's up a time for datetime datatype -
later I found out that DateOnly datatype in C# is not supported directly via Entity Framework so I decided to stick with this datatype.
