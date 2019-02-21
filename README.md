# MovieTheater
Order Tickets Simulation
```
var op = new OrderProcessor();
Section section = new Section(rows:10,cols:5,Location.Front);//Location is an enum
List<Section> sections = new List<Section>{
  section,
  };
Theater t = new Theater(
            	sections,
            	name:"A1",
            	startTime:new DateTime(
            		year:DateTime.Now.Year,
            		month:12,
            		day:1,
            		hour:5,
            		minute:0,
            		second:0),
            	title:"That One Movie Title"
            	);
            	
            var kiosk = new Kiosk(
            	theaters: new List<Theater>{
            		t
            });
            
            kiosk.OrderTicket(new CustomerOrder{
            	Title = "That One Movie Title",
            	MovieTime = new DateTime(DateTime.Now.Year,12,1,5,0,0),
            	Section = 0,
            	Row = 3,
            	Col = 2
            });
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

 
