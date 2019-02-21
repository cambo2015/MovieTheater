# MovieTheater
Order Tickets Simulation
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
- CLASSES: 
   - `OrderProcessor()` : when tickets purchased a theater event signals the order processor to confirm purchase and check if seat has been taken. Purchases are NOT made here but in the Theater class.
   - `Section()` : a movie theater has multiple sections on where to sit. Some in the front,back,right,left,balcony,etc. 
   - `Theater()` : is the movie theater. Tickets are purchased here.

- ENUMS:
   - `Location` : used in the Section class. A theater has many sections in which a person can choose to sit. This enum was made such that it can be used in different theater settings such as a Broadway show or Imax theater.
       - `Left`,
       - `Right`,
       - `Front`,
       - `Back`, 


### To be added

 - [ ] specify movie times and what time ticket was ordered

 
