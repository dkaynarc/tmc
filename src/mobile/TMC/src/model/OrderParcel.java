package model;

import java.util.ArrayList;

public class OrderParcel {
	
	
	private ArrayList<Order> orders = new ArrayList<Order>();
	
	
	
	
	public void setOrders(ArrayList<Order> orders)
	{
		this.orders  = orders;
	}
	
	
	public ArrayList<Order> getOrders()
	{		
		return this.orders;
	}

}
