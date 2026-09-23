import React, { useState, useEffect } from 'react';
import { claimService, expertiseService } from './services/api';
import './App.css';

function App() {
  const [claims, setClaims] = useState([]);
  const [expertises, setExpertises] = useState([]);
  const [policyNumber, setPolicyNumber] = useState('');
  const [description, setDescription] = useState('');
  const [loading, setLoading] = useState(false);

  const fetchData = async () => {
    try {
      const [claimsRes, expertisesRes] = await Promise.all([
        claimService.getAll().catch(() => ({ data: [] })),
        expertiseService.getAll().catch(() => ({ data: [] }))
      ]);
      setClaims(claimsRes.data || []);
      setExpertises(expertisesRes.data || []);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!policyNumber || !description) return;

    setLoading(true);
    try {
      await claimService.create({ policyNumber, description });
      setPolicyNumber('');
      setDescription('');
      await fetchData();
    } catch (err) {
      alert("Erreur lors de la création du sinistre.");
    } finally {
      setLoading(false);
    }
  };

  return (
      <div className="dashboard">
        <header className="header">
          <div className="brand">
            <span className="brand-icon">⚡</span>
            <div>
              <h1>FastEx Operations</h1>
              <p className="subtitle">Microservices Platform • Claim.API (5001) & Expertise.API (5002)</p>
            </div>
          </div>
          <button onClick={fetchData} className="btn-refresh">🔄 Rafraîchir</button>
        </header>

        <div className="grid-container">
          {/* Formulaire */}
          <section className="card">
            <h2 className="card-title">✍️ Nouveau Sinistre</h2>
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label>N° Police / Contrat</label>
                <input
                    type="text"
                    placeholder="ex: FAST-777"
                    value={policyNumber}
                    onChange={(e) => setPolicyNumber(e.target.value)}
                    required
                />
              </div>
              <div className="form-group">
                <label>Description des dégâts</label>
                <textarea
                    placeholder="ex: Accident véhicule de société..."
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    required
                />
              </div>
              <button type="submit" disabled={loading} className="btn-submit">
                {loading ? "Création..." : "Déclarer le Sinistre"}
              </button>
            </form>
          </section>

          {/* Liste des Sinistres */}
          <section className="card">
            <h2 className="card-title">📋 Sinistres Déclarés ({claims.length})</h2>
            {claims.length === 0 ? (
                <p className="empty-state">Aucun sinistre enregistré pour le moment.</p>
            ) : (
                <div className="item-list">
                  {claims.map((c, i) => (
                      <div key={i} className="item-card">
                        <div>
                          <span className="policy-tag">#{c.policyNumber}</span>
                          <p className="item-desc">{c.description}</p>
                        </div>
                        <span className="badge badge-blue">{c.status || 'Enregistré'}</span>
                      </div>
                  ))}
                </div>
            )}
          </section>

          {/* Tableau d'Expertises */}
          <section className="card full-width">
            <h2 className="card-title">🛠️ Missions d'Expertise Générées</h2>
            {expertises.length === 0 ? (
                <p className="empty-state">Aucune mission d'expertise active.</p>
            ) : (
                <div className="table-container">
                  <table>
                    <thead>
                    <tr>
                      <th>ID Mission</th>
                      <th>Ref Sinistre / Police</th>
                      <th>Statut</th>
                      <th>Horodatage</th>
                    </tr>
                    </thead>
                    <tbody>
                    {expertises.map((e, idx) => (
                        <tr key={idx}>
                          <td>#{e.id || idx + 1}</td>
                          <td>{e.claimId || e.policyNumber || 'FAST-AUTOMATIC'}</td>
                          <td><span className="badge badge-green">{e.status || 'Assignée'}</span></td>
                          <td>{e.createdAt ? new Date(e.createdAt).toLocaleString() : 'Récemment'}</td>
                        </tr>
                    ))}
                    </tbody>
                  </table>
                </div>
            )}
          </section>
        </div>
      </div>
  );
}

export default App;