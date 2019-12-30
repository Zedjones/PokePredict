import pika
import json

connection = pika.BlockingConnection(
    pika.ConnectionParameters(host="localhost")
)
channel = connection.channel()
channel.queue_declare(queue="pokemon")

def callback(ch, method, properies, body):
    stats_list = json.loads(body)
    ch.basic_ack(delivery_tag=method.delivery_tag)
    print(stats_list)

channel.basic_consume(queue="pokemon", on_message_callback=callback)
channel.start_consuming()