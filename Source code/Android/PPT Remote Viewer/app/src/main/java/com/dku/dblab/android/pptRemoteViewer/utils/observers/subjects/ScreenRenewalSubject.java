package com.dku.dblab.android.pptRemoteViewer.utils.observers.subjects;

import com.dku.dblab.android.pptRemoteViewer.utils.observers.ScreenRenewalObserver;

import java.util.ArrayList;

public abstract class ScreenRenewalSubject {
	protected ArrayList<ScreenRenewalObserver> observers = new ArrayList<ScreenRenewalObserver>();
	
	public void addObserver(ScreenRenewalObserver observer) {
		observers.add(observer);
	}
	
	public void removeObserver(ScreenRenewalObserver observer) {
		observers.remove(observer);
	}
	
	public abstract void notify(Object parameter);
}
