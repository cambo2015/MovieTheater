# MovieTheater
Order Movie Tickets Simulation
```
var op = new OrderProcessor();
Section section = new Section(rows,cols,Location.Front);//Location is an enum
List<Section> sections = new List<Section>{
  section,
  };
Theater t = new Theater(sections,name:"A");
t.SecureSeat(section:0,row:3,col:5);
```
  
## Additional Information:
* classes: 
-OrderProcessor : when tickets purchased a theater event signals the order processor to confirm purchase and check if seat has been taken
-Section : a movie theater has multiple sections on where to sit. Some in the front,back,right,left,balcony,etc. 
-Theater : is the movie theater

* enum:
-Location : used in the Section class. A theater has many sections in which a person can choose to sit. Left,Right,Front,Back. This enum was created for multiple use cases:IMAX,OPERA THEATER,BROADWAY. 


 
