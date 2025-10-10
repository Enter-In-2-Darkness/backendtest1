import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL || 'https://backendtest1-4bao.onrender.com/api/product';

const api = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json'
  }
});

export default {
  async getAll() {
    const response = await api.get('/');
    return response.data.data || [];
  },

  async create(code) {
    const response = await api.post('/', { numberCode: code });
    return response.data.data;
  },

  async delete(id) {
    const response = await api.delete(`/${id}`);
    return response.data;
  }
};
