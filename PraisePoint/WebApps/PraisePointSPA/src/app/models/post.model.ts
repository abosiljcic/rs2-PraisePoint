export interface Post {
  id: string;
  senderUsername: string;
  receiverUsername: string;
  companyId: string;
  points: number;
  description: string;
  createdDate: string;
  postComments: {
    id: string;
    username: string;
    text: string;
    createdDate: string;
  }[];
  postLikes: {
    id: string;
    username: string;
  }[];
}
