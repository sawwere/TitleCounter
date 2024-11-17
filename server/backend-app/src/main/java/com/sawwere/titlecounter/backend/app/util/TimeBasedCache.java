package com.sawwere.titlecounter.backend.app.util;

import java.util.Objects;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

public class TimeBasedCache<K, V> {
    private final ConcurrentHashMap<K, Entry> hashMap = new ConcurrentHashMap<>();
    private final long defaultExpirationTimeout;

    private ScheduledExecutorService scheduler = Executors.newSingleThreadScheduledExecutor(r -> {
        Thread th = new Thread(r);
        th.setDaemon(true);
        return th;
    });

    @SuppressWarnings("checkstyle:MagicNumber")
    public TimeBasedCache(long refreshTimeout, long defaultExpirationTimeout) {
        if (refreshTimeout < 1000) {
            throw new IllegalArgumentException("Too short time interval for scheduling");
        }

        this.defaultExpirationTimeout = defaultExpirationTimeout;

        scheduler.scheduleAtFixedRate(() -> {
                long current = System.currentTimeMillis();
                for (var key : hashMap.keySet()) {
                    if (current - hashMap.get(key).getExpiresAt() < 0) {
                        hashMap.remove(key);
                    }
                }
            }, 1, refreshTimeout, TimeUnit.MILLISECONDS);
    }

    @SuppressWarnings("checkstyle:MagicNumber")
    public TimeBasedCache(long refreshTimeout) {
        this(refreshTimeout, 1000L);
    }

    public V get(K key) {
        var actual = hashMap.get(key);
        return actual == null ? null : actual.getValue();
    }

    public void put(K key, V value, long timeout) {
        hashMap.put(key, new Entry(value, System.currentTimeMillis() + timeout));
    }

    public void put(K key, V value) {
        put(key, value, defaultExpirationTimeout);
    }

    public V remove(K key) {
        return hashMap.remove(key).getValue();
    }

    private final class Entry {
        private final V value;

        public long getExpiresAt() {
            return expiresAt;
        }

        private final long expiresAt;

        private Entry(V value, long expiresAt) {
            this.value = value;
            this.expiresAt = expiresAt;
        }

        public V getValue() {
            return value;
        }

        @Override
        public boolean equals(Object o) {
            if (this == o) {
                return true;
            }
            if (o == null || getClass() != o.getClass()) {
                return false;
            }
            Entry entry = (Entry) o;
            return Objects.equals(value, entry.value);
        }

        @Override
        public int hashCode() {
            return Objects.hash(value);
        }
    }
}
