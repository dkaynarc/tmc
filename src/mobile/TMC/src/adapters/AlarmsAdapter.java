
package adapters;
import ictd.activities.R;
import java.util.ArrayList;
import model.Alarm;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;


public class AlarmsAdapter extends ArrayAdapter<Alarm>
{
	ArrayList<Alarm> mObjects = null;


	public AlarmsAdapter(Context context, int resource,	ArrayList<Alarm> arrayList)
	{
		super(context, resource, arrayList);
		mObjects = arrayList;
	}


	@Override
	public View getView(int position, View convertView, ViewGroup parent)
	{
		View v = convertView;
		if (v == null)
		{
			LayoutInflater vi = (LayoutInflater) getContext().getSystemService(	Context.LAYOUT_INFLATER_SERVICE);
			v = vi.inflate(R.layout.alarm_row, null);
		}
		
		Alarm alarm = mObjects.get(position);
		if (alarm != null)
		{
			TextView id = (TextView) v
					.findViewById(R.id.alarm_id_value);
			TextView type = (TextView) v
					.findViewById(R.id.alarm_type_value);
		    TextView time = (TextView) v
					.findViewById(R.id.alarm_time_value);
			TextView description = (TextView) v
					.findViewById(R.id.alarm_description_value);
			
				id.setText(alarm.getId());
				type.setText(alarm.getType());
				time.setText(alarm.getTime());
				description.setText(alarm.getDescription());
		}
		else
			v = convertView;
		return v;
	}
}
