package Services;


import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.HttpURLConnection;
import java.net.Socket;
import java.net.URL;
import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserFactory;
import android.R.string;
import android.app.IntentService;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;



public class SynchService extends IntentService 
{
	public static final String FEEDBACK = "FEEDBACK";
	
    
    private String command;
    private String data;
    private BufferedInputStream in;
    private PrintWriter out;
    private	String remoteIp;


    public final static String urlString = "http://172.19.199.132:9000/api/Server/6";


    public SynchService()
    {
		super("SynchService");
	}
    

	@Override
	protected void onHandleIntent(Intent intent) 
	{
		
		Bundle parcel = intent.getBundleExtra("parcel");
	    data = parcel.getString("data");
	    command = parcel.getString("command");
	    StringBuilder str = new StringBuilder();
	    
	    try
	    {
	    	URL url = new URL(urlString);
	    	HttpURLConnection urlConnection = (HttpURLConnection) url.openConnection();
	    	urlConnection.setRequestMethod("GET");
         	in = new BufferedInputStream(urlConnection.getInputStream());
	    
	    	
	        //XmlPullParser parser = XmlPullParserFactory.newInstance().newPullParser();
	    	//parser.setFeature(XmlPullParser.FEATURE_PROCESS_NAMESPACES, false);
	    	//parser.setInput(in, null);
	    	//int eventType = parser.getEventType();
	    	byte[] contents = new byte[1024];
	    	int flag;
	
	    	 
	    	while((flag = in.read(contents)) !=-1 )
	    	{
	    	   str.append(new String(contents,0,flag));
	    	}
	    	
	    	//while(eventType != XmlPullParser.END_DOCUMENT)
	    //	{
	    	//	buf.append(parser.getText());
	    	//	eventType =  parser.next();	
	    //	}
	    	
	    	
	    }
	    catch(Exception ec)
	    {
	       String exc = ec.toString();	    
	    }
	    
	    
	    
	    
	    
		
		  notifyMainActivity(str.toString());
		  
	}

	



	
	
private void notifyMainActivity(String data) 
{
	// send some data to the receiver
	//try
//	{  
	  // String command = in.readLine();
	  // String result = in.readLine();
	  // String data = in.readLine();
	  // System.out.println("sending to the handler " + result);
	   
	   
	   Intent intent = new Intent();
	   intent.setAction(FEEDBACK);
	   intent.putExtra("command", command);
	  // intent.putExtra(command, result); 
	   intent.putExtra("data", data);
	   
	   sendBroadcast(intent);
	//}
//	catch(IOException e){}
}

}


