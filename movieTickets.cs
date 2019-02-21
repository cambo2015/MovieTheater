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
            	name:"A1"
            	);
            	
            t.SecureSeat(
            	section:0,
            	row:3,
            	col:5
            	);
        
        	t.SecureSeat(//ordering a seat that is taken->SEAT ALREADY TAKEN
            	section:0,
            	row:3,
            	col:5
            	);
            	
            //s1.Show();
            t.ShowAllSeats();
            	
             
             
            
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
    
    	 public string Name{
     		get;set;
     	}
        private List<Section> sections;
        public Theater(List<Section> sections,string name)
        {
            this.sections = sections;
            Name = name;
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
    
