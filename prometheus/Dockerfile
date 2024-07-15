FROM prom/prometheus:v2.22.0 as image-prometheus-base
COPY ./prometheus.yml /etc/prometheus

VOLUME ./data
EXPOSE 9090