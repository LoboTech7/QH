"use client";

import { useEffect, useState } from "react";

type Client = {
  clientId: number;
  address: string;
  phoneNumber: string;
  status: string;
};

export default function Home() {
  const [clients, setClients] = useState<Client[]>([]);

  useEffect(() => {
    fetch("http://localhost:5063/api/client")
      .then((res) => res.json())
      .then((data) => setClients(data));
  }, []);

  return (
    <div className="p-8">
      <h1 className="text-2xl font-bold mb-4">Clients</h1>
      <ul className="flex flex-col gap-2">
        {clients.map((client) => (
          <li key={client.clientId} className="border rounded p-4">
            <p><span className="font-semibold">Address:</span> {client.address}</p>
            <p><span className="font-semibold">Phone:</span> {client.phoneNumber}</p>
            <p><span className="font-semibold">Status:</span> {client.status}</p>
          </li>
        ))}
      </ul>
    </div>
  );
}
