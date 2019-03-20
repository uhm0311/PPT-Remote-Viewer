package com.dku.dblab.android.pptRemoteViewer.utils.observers.subjects;

import android.graphics.Bitmap;

public class ScreenRenewalNotifier extends ScreenRenewalSubject {

	@Override
	public void notify(Object parameter) {
		Bitmap temp = null;
		
		if (parameter instanceof Bitmap) {
			temp = (Bitmap)parameter;
		}
		
		for (int i = 0; i < observers.size(); i++) {
			observers.get(i).onScreenChanged(temp);
		}
	}

}
