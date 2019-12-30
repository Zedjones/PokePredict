import pika
import json

connection = pika.BlockingConnection(
    pika.ConnectionParameters(host="localhost")
)
channel = connection.channel()
channel.queue_declare(queue="pokemon")

def callback(ch, method, properies, body):
    stats_list = json.loads(body)
    print(stats_list)

channel.basic_consume(queue="pokemon", on_message_callback=callback, auto_ack=True)
channel.start_consuming()