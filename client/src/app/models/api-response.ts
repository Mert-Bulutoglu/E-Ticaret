export interface ApiResponse {
    apiVersion: string,
    data: any | undefined | null,
    error: any | undefined | null,
    success: boolean
}