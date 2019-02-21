using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Dcoder
{
    class Program
    {
        static void Main(string[] args)
        {
        	var op = new OrderProcessor();
           WriteLine(op);
        	
            Section s1 = new Section(5,6,Location.Front);
            Section s2 = new Section(5,6,Location.Back);
            var sections = new List<Section>
            {
            	s1,
            	s2
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
            
            kiosk.OrderTicket(new CustomerOrder{
            	Title = "That One Movie Title",
            	MovieTime = new DateTime(DateTime.Now.Year,12,1,5,0,0),
            	Section = 0,
            	Row = 3,
            	Col = 2
            });
            
            	
            
            /*t.SecureSeat(
            	section:0,
            	row:3,
            	col:5
            	);
        
        	t.SecureSeat(//ordering a seat that is taken->SEAT ALREADY TAKEN
            	section:0,
            	row:3,
            	col:5
            	);*/
            	
            //s1.Show();//DO NOT USE
            t.ShowAllSeats();//USE THIS INSTEAD
        }
    }
    
    public struct CustomerOrder
    {
    	public string Title
    	{
    		 get;set;
    	}
    	public DateTime MovieTime
    	{
    		 get;set;
    	}
    	public int Section
    	{
    		get;set;
    	}
    	
    	public int Row
    	{
    		get;set;
    	}
    	public int Col
    	{
    		get;set;
    	}
    	
    	public float RewardsPoints{
    		 get;set;
    	}
    	
    	public float GiftCardAmount{
    		 get;set;
    	}
    	
    	public int Age{
    		get;set;
    	}
    }
    
    
    public class Kiosk
    {
    	public static double AdmissionPrice{get;set;} = 12.99;
    	List<Theater> Theaters{
    		get; set;
    	}
    	
    	public Kiosk(List<Theater> theaters)
    	{
    		
    		Theaters = theaters;
    	}
    	
    	public void OrderTicket(CustomerOrder custOrder)
    	{
    		List<Theater> t = Theaters.Where(x=> x.Title == custOrder.Title && x.StartTime == custOrder.MovieTime ).ToList();
    		t.ForEach(x=>WriteLine(x));
    		if(t != null)
    		{
    			t[0].SecureSeat(section:custOrder.Section,row:custOrder.Row,col:custOrder.Col);
    		} 			
    	}
    	
    	
    }
    
    
    public class OrderProcessor
    {
    	
    	public OrderProcessor()
    	{
    		Theater.MyEvent += OrderProcessed;
    	}
    	
    	~OrderProcessor()
    	{
    		Theater.MyEvent -= OrderProcessed;
    	}
    	public void OrderProcessed(object sender,SeatEventArgs e)
    	{
    		Write("Attempting to order a seat: ");
    		string s = (e.SeatAvailibility !=0 )? 
    			"Success! Seat Secured!":"Error: Seat all ready taken";
    		WriteLine(s);
    		//WriteLine("hello world");
    	}
    }
    
    public enum Location
    {
    	Front,
    	Back,
    	Left,
    	Right,
    }
    
    public struct Seat
    {
        public bool CanVibrate{get;set;}
        public int Available{get;set;}
    }
    
    public class Section :IEnumerable
    {
    	
        public Seat[,] seats{
        	get;set;
        }
        public Location location{get;set;}
        
        int rows,cols;
        
        public Section(int rows,int cols,Location location)
        {
            seats = new Seat[rows,cols];
            this.rows = rows;
            this.cols = cols;
            this.location = location;
        }
        
        public IEnumerator GetEnumerator()
        {
            foreach(var seat in seats)
                yield return seat;
        }
        
        public void Show()
        {
        	WriteLine();
        	WriteLine(location.ToString("g"));
            for(int i = 0;i<rows;i++)
            {
                for(int j = 0;j<cols;j++)
                {
                    if(j%cols ==0)
                    {
                        WriteLine(" ");
                    }
                    Write("["+seats[i,j].Available +"]");
                }
            }
            WriteLine();
        }
    }
    
    public class Theater
    {
    	
    	public delegate void SeatOrderedEventHandler(object sender,SeatEventArgs e);
    	public static event SeatOrderedEventHandler MyEvent; 
    
    	 public string TheaterName{
     		get;set;
     	}
     	public string Title{
     		get;set;
     	}
     	
     	public DateTime StartTime{
     		get;set;
     	}
        private List<Section> sections;
        public Theater(List<Section> sections,string name,DateTime startTime,string title)
        {
            this.sections = sections;
            TheaterName = name;
            StartTime = startTime;
            Title = title;
        }
        
        public void SecureSeat(int section,int row,int col)
        {
        	 
        	int isAvailable = 0;
        	ref Seat s = ref sections[section].seats[row,col];
        	if (s.Available !=1)
        	{
        		s.Available = 1;
        		isAvailable = 1;
        	}else{
        		isAvailable = 0;
        	}
        	
        	OnSeatOrdered(isAvailable);
        }
        
        public void OnSeatOrdered(int available)
        {
        	if(MyEvent != null)
        	{
        		MyEvent(this,new SeatEventArgs{
        			SeatAvailibility = available
        		});
        	}
        	else
        	{
        		WriteLine("No Subscribers. Event not fired");
        	}
        }
        
        public void ShowAllSeats()
        {
        	WriteLine();
        	sections.ForEach(x=>x.Show());
        	WriteLine();
        }
    }
    
    public class SeatEventArgs: EventArgs
    {
    	public int SeatAvailibility{
    		get;set;
    	}
    }
}
    
    