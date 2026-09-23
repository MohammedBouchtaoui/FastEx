import axios from 'axios';

const CLAIM_API_URL = 'http://localhost:5001/api';
const EXPERTISE_API_URL = 'http://localhost:5002/api';

export const claimService = {
    getAll: () => axios.get(`${CLAIM_API_URL}/claims`),
    create: (claimData) => axios.post(`${CLAIM_API_URL}/claims`, claimData),
};

export const expertiseService = {
    getAll: () => axios.get(`${EXPERTISE_API_URL}/expertises`),
};