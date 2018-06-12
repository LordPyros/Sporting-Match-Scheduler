# Sporting-Match-Scheduler
  I was very new to coding when I created this app, to be honest the code is pretty terrible. I uploaded this project to display my problem solving ability and what I was able to achieve with very little knowlege of c#. My coding skills have come a very long way since then. Below is a list of many things that I would do differently if I were to make this application again.


  This was my first attempt at making a real world application. It was intended to assist and save time for an Indoor Sports 
center in which I am a member of. It allows the user to create a weekly schedule for matches and manage teams accross several indoor sports.  
  

GETTING STARTED
  
  Once you have selected a sport and created a new league, I recommend setting up game times first.  Doing this may save time as you won't need to revisit options to setup the rest of the competition.


FEATURES

Teams will play other teams an even amount of times (round robin).

Teams will play at all available timeslots an even amount of times (roughly).

Settings exist to prevent teams from being scheduled to play at times they are unavailable for.
  eg. Vipers can't play 6PM games, Cougars can't play 8.30PM games etc.
Ability to set game times and number of courts available at each time.

Byes are automatically issued to the teams that have played the most, in situations where there are too many teams 
  for the available timeslots.
  
Teams can manually be given byes for occasions they are unable to play.

Matches can be made (and removed) manually (for finals, special requests, forfeits, etc).

Statists are available to show how many times each team has played other teams and how many times they've played
  at each timeslot.
  
Ability to undo a roster incase of user error or unexpected changes.

Add / remove / rename leagues.

Add / remove / rename teams at any time throught a season without causing an issue.



THINGS I WOULD DO DIFFERENTLY IF I WERE TO MAKE THIS APPLICATION AGAIN.

The program has many global varibales as I had not got my head around passing varibles to methods.

Ints are used in many placed where I should have used bools.

I used lots of .CSV files for storage instead of using an sql database.

I created serveral error forms instead of having just one and passing the error message to it.

There should be many more error warnings for incorrect input, instead buttons do nothing unless a valid input is entered.

I have got some naming conventions wrong.

I did not organise forms and classes into appropriate folders, instead everything is in the program root.

Variables and methods are often named poorly.

Some of the wording in the UI could be improved eg. Some 'Done' buttons could be changed to 'Sumbit' etc. 

Many sections of code lack commenting.

A lot more refactoring could be done to clear out repeated code.

The print feature was not coded.

The LSOP (last seasons overall position) feature needs implementing to shuffle the teams each season so that if a team has
  to play another team twice in a season, its likely that the team they have to play is of similar skill.
    eg The top ranked team from last season is far more likely to play the 2nd or 3rd ranked team twice, rather than having
    to play the 14th ranked team twice.
    
Due to many reasons (eg number of teams, teams availabilty, byes, etc) it is impossible to make sure every team plays every 
  other team before they play a team twice. Work needs to be done to make sure teams have played everyone before they have 
  played too many teams twice.
  




