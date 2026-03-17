import { create } from "zustand";

type State = {
  pageNumber: number;
  pageSize: number;
  searchTerm: string;
  orderBy: string;
  filterBy: string;
  seller?: string;
  winner?: string;
};

type Action = {
  setParam: (param: Partial<State>) => void;
  reset: () => void;
};

const initialState: State = {
  pageNumber: 1,
  pageSize: 16,
  searchTerm: "",
  orderBy: "",
  filterBy: "",
  seller: undefined,
  winner: undefined,
};

export const useParamStore = create<State & Action>((set) => ({
  ...initialState,
  setParam: (newParam: Partial<State>) => {
    set((state) => {
      if (newParam.pageNumber) {
        return { ...state, pageNumber: newParam.pageNumber };
      } else {
        return { ...state, ...newParam, pageNumber: 1 };
      }
    });
  },
  reset: () => set(initialState),
}));
